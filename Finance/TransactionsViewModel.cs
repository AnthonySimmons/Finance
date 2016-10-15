using FinanceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class TransactionsViewModel
    {
        public ObservableCollection<DataPoint> Transactions { get; set; }

        private TransactionLoader _transactionLoader;

        public TransactionsViewModel()
        {
            _transactionLoader = new TransactionLoader();
            LoadTransactions();
            
        }

        private void LoadTransactions()
        {
            Transactions = new ObservableCollection<DataPoint>();
            var transactions = _transactionLoader.Load(@"C:\Users\antho\Downloads\export.qif");
            foreach(var transaction in transactions)
            {
                var dataPoint = new DataPoint
                {
                    X = transaction.DateTime.Day,
                    Y = transaction.Amount
                };
                Transactions.Add(dataPoint);
            }

        }
    }
}
