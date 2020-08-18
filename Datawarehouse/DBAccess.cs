using System;
using System.Linq;

namespace Datawarehouse
{
    public class DBAccess
    {
        private Entities db;

        public DBAccess()
        {
            db = new Entities();
        }

        public void Write(Stage stage)
        {
            using (var context = new Entities())
            {
                CreateSale(stage, context);
            }
        }

        /**
         * Create new sale
         */
        private void CreateSale(Stage stage, Entities context)
        {
            var sales = new Sales
            {
                seller_FK = GetSeller(stage, context),
                product_FK = GetProduct(stage, context),
                city_FK = GetCity(stage, context),
                pos_FK = GetPos(stage, context),
                date = stage.Date,
                amount = stage.Amount,
                price = Convert.ToDecimal(stage.Price)
            };

            db.Sales.Add(sales);
            db.SaveChanges();
        }

        private int GetSeller(Stage stage, Entities context)
        {
            var seller = context.Sellers.FirstOrDefault(data => data.name.Contains(stage.Seller));

            if (seller != null)
            {
                return seller.PK;
            }

            seller = new Sellers
            {
                name = stage.Seller
            };
            
            db.Sellers.Add(seller);
            db.SaveChanges();
            
            return seller.PK;
        }

        private int GetProduct(Stage stage, Entities context)
        {
            var product = context.Products.FirstOrDefault(data => data.name.Contains(stage.Product));

            if (product != null)
            {
                return product.PK;
            }

            product = new Products
            {
                name = stage.Product
            };
            
            db.Products.Add(product);
            db.SaveChanges();
            
            return product.PK;
        }

        private int GetPos(Stage stage, Entities context)
        {
            var pos = context.PointOfSales.FirstOrDefault(data => data.name.Contains(stage.Pos));

            if (pos != null)
            {
                return pos.PK;
            }

            pos = new PointOfSales
            {
                name = stage.Pos
            };
            
            db.PointOfSales.Add(pos);
            db.SaveChanges();
            
            return pos.PK;
        }

        private int GetCity(Stage stage, Entities context)
        {
            var city = context.Cities.FirstOrDefault(data => data.name.Contains(stage.City));

            if (city != null)
            {
                return city.PK;
            }

            city = new Cities
            {
                name = stage.City
            };
            
            db.Cities.Add(city);
            db.SaveChanges();
            
            return city.PK;
        }
    }
}