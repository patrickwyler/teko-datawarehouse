using System.Collections.Generic;

namespace Datawarehouse
{
    public class JsonRepresentation
    {
        private List<Transactions> transactions;

        public List<Transactions> Transactions
        {
            get { return transactions; }
            set { transactions = value; }
        }
    }
}