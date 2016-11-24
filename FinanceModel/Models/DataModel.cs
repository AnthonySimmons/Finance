using System.Linq;
using System;
using System.ComponentModel;

namespace FinanceModel.Models
{
    public class DataModel : INotifyPropertyChanged
    {
        private ITransactionLoader _transactionLoader;

        private TransactionCollection _transactions = new TransactionCollection();

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DataModel(ITransactionLoader transactionLoader)
        {
            _transactionLoader = transactionLoader;
            _transactions.CollectionChanged += _transactions_CollectionChanged;
        }

        private void _transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Transactions));
        }

        public void Clear()
        {
            _transactions.Clear();
        }
        
        
        public void LoadDataFromFile(string dataFilePath)
        {
            try
            {
                Transactions.AddRange(_transactionLoader.Load(dataFilePath));
                StartDate = Transactions.StartDate;
                EndDate = Transactions.EndDate;
            }
            catch (Exception ex)
            {
                Logger.Logs.Instance.Log(ex);
            }
        }

        public TransactionCollection Transactions => _transactions;
     
        public DateTime GetEarliestTransaction()
        {
            DateTime val = DateTime.Now;
            Transaction tran = Transactions.OrderBy(t => t.Date).FirstOrDefault();
            if(tran != null && tran.Date > DateTime.MinValue)
            {
                val = tran.Date;
            }
            return val;
        }
        
        public DateTime GetLatestTransaction()
        {
            DateTime val = DateTime.Now;
            Transaction tran = Transactions.OrderByDescending(t => t.Date).FirstOrDefault();
            if(tran != null)
            {
                val = tran.Date;
            }
            return val;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
