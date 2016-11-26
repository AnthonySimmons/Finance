using System.Linq;
using System;
using System.ComponentModel;

namespace FinanceModel.Models
{
    public class DataModel : INotifyPropertyChanged
    {
        private ITransactionLoader _transactionLoader;

        private ITransactionSaver _transactionSaver;

        private readonly TransactionCollection _transactions = new TransactionCollection();

        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if(value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        private string _dataFilePath;
        public string DataFilePath
        {
            get { return _dataFilePath; }
            private set
            {
                if (value != _dataFilePath)
                {
                    _dataFilePath = value;
                    OnPropertyChanged(nameof(DataFilePath));
                }
            }
        }
        
        public DataModel(ITransactionLoader transactionLoader, ITransactionSaver transactionSaver)
        {
            _transactionLoader = transactionLoader;
            _transactionSaver = transactionSaver;
            _transactions.TransactionsChanged += _transactions_TransactionsChanged;
        }

        private void _transactions_TransactionsChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Transactions));
        }
        
        public void Clear()
        {
            _transactions.Clear();
        }
        
        public void Refresh()
        {
            if(string.IsNullOrWhiteSpace(DataFilePath))
            {
                throw new InvalidOperationException("No Data File Path defined");
            }
            Clear();
            LoadDataFromFile(DataFilePath);
        }
        
        public void LoadDataFromFile(string dataFilePath)
        {
            try
            {
                DataFilePath = dataFilePath;
                var transactions = _transactionLoader.Load(dataFilePath);
                Transactions.AddRange(transactions);
                StartDate = Transactions.StartDate;
                EndDate = Transactions.EndDate;
            }
            catch (Exception ex)
            {
                Logger.Logs.Instance.Log(ex);
            }
        }

        public void SaveDataToFile(string dataFilePath)
        {
            try
            {
                _transactionSaver.Save(_transactions, dataFilePath);
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
            Transaction tran = Transactions.OrderBy(t => t.Date).FirstOrDefault(t => t.Date > DateTime.MinValue);
            if(tran != null)
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
