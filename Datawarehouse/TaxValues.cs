using System.Collections.Generic;

namespace Datawarehouse
{
    // Singleton class for tax values
    public class Tax
    {
        private static Tax _instance;

        private Dictionary<string, double> _values = new Dictionary<string, double>
        {
            {"Bern", 0.923},
            {"Wien", 0.8}
        };

        private Tax()
        {
        }

        public static Tax Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Tax();
                }

                return _instance;
            }
        }


        public double GetValue(string city)
        {
            return _values[city];
        }
    }
}