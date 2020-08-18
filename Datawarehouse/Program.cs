using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using static Newtonsoft.Json.JsonConvert;

namespace Datawarehouse
{
    class Program
    {
        private const string PathToFile = "C:/Temp/transactions.json";
        private const string SwissDateFormat = "dd.MM.yyyy";

        public static void Main(string[] args)
        {
            Console.WriteLine("Start");

            // First step: extract data
            var extractionStage = Extract();

            // Second step: transform data
            var transformationStage = Transform(extractionStage);

            // Third step: load data to database
            Load(transformationStage);

            Console.WriteLine("Finish!");
            Console.Read();
        }

        /*
         * Extract data from json => object
         */
        private static List<Stage> Extract()
        {
            Console.WriteLine("...Extract");

            List<Stage> extractionStage = new List<Stage>();

            try
            {
                using (StreamReader stream = new StreamReader(PathToFile))
                {
                    JsonRepresentation ConvertJsonToObject(string jsonString)
                    {
                        var jsonRep = DeserializeObject<JsonRepresentation>(jsonString,
                            new IsoDateTimeConverter {DateTimeFormat = SwissDateFormat});
                        return jsonRep;
                    }

                    List<Stage> MergeSourceToTarget(List<Transactions> transactions)
                    {
                        var target = new List<Stage>();

                        foreach (var transaction in transactions)
                        {
                            var stage = new Stage
                            {
                                Date = transaction.Date,
                                City = transaction.City,
                                Pos = transaction.Pos,
                                Product = transaction.Product,
                                Amount = transaction.Amount,
                                Price = transaction.Price,
                                Seller = transaction.Seller
                            };

                            target.Add(stage);
                        }

                        return target;
                    }

                    // read stream to string
                    var line = stream.ReadToEnd();

                    // convert json => object
                    var jsonRepresentation = ConvertJsonToObject(line);

                    // merge transactions to extraction stage
                    extractionStage = MergeSourceToTarget(jsonRepresentation.Transactions);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
            }

            return extractionStage;
        }

        /*
         * Transform items (recalculate price without taxes)
         */
        private static List<Stage> Transform(List<Stage> source)
        {
            Console.WriteLine("...Transform");

            var target = new List<Stage>();

            foreach (var entry in source)
            {
                CalculateTaxFreePrice(entry);

                target.Add(entry);
            }

            return target;
        }

        /*
         * Load database with transformed items
         */
        private static void Load(List<Stage> transformationStage)
        {
            Console.WriteLine("...Load");

            var db = new DBAccess();

            foreach (var item in transformationStage)
            {
                db.Write(item);
            }
        }

        private static void CalculateTaxFreePrice(Stage entry)
        {
            var price = entry.Price;
            var taxValue = Tax.Instance.GetValue(entry.City);
            entry.Price = price * taxValue;
        }
    }
}