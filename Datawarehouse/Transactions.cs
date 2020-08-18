using System;

namespace Datawarehouse
{
    public class Transactions
    {
        private DateTime _date;
        private string _city;
        private string _pos;
        private string _product;
        private int _amount;
        private double _price;
        private string _seller;

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public string Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Seller
        {
            get { return _seller; }
            set { _seller = value; }
        }
    }
}