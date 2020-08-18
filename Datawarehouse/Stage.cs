using System;

namespace Datawarehouse
{
    public class Stage
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
            get => _city;
            set => _city = value;
        }

        public string Pos
        {
            get => _pos;
            set => _pos = value;
        }

        public string Product
        {
            get => _product;
            set => _product = value;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public double Price
        {
            get => _price;
            set => _price = value;
        }

        public string Seller
        {
            get => _seller;
            set => _seller = value;
        }
    }
}