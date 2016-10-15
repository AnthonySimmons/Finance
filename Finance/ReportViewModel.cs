using FinanceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DataPoint> _dataPoints = new ObservableCollection<DataPoint>();
        public ObservableCollection<DataPoint> DataPoints
        {
            get
            {
                return _dataPoints;
            }
            set
            {
                if (_dataPoints != value)
                {
                    _dataPoints = value;
                    OnPropertyChanged(nameof(DataPoints));
                }
            }
        }
        
        private ReportModel _dataModel;

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
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                    OnPropertyChanged(nameof(StartOADate));                   
                }
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if(_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                    OnPropertyChanged(nameof(EndOADate));
                }
            }
        }

        public double StartOADate
        {
            get
            {
                return StartDate.ToOADate();
            }
            set { OnPropertyChanged(nameof(StartOADate)); }
        }

        public double EndOADate
        {
            get
            {
                return EndDate.ToOADate();
            }
            set { }
        }
        
        public ReportViewModel()
        {
            LoadTransactions();
            StartDate = new DateTime(2016, 10, 1);
            EndDate = DateTime.Now;
        }
        

        private void LoadTransactions()
        {
            _dataModel = new ReportModel(new QuickenExpenseLoader());
            LoadDataFromFolder(Config.ExpensesDirectoryPath);
        }
        
        public void LoadDataFromFolder(params string [] filePaths)
        {
            _dataModel.Clear();
            foreach (var filepath in filePaths)
            {
                _dataModel.LoadDataFromFolder(filepath);
            }
            LoadTotalExpenses();
        }

        public void LoadDataFromFiles(params string[] filePaths)
        {
            _dataModel.Clear();
            foreach (var filepath in filePaths)
            {
                _dataModel.LoadDataFromFile(filepath);
            }
            LoadTotalExpenses();
        }

        private IEnumerable<Transaction> GetFilteredTransactions()
        {
            var transactions = _dataModel.Transactions.Where(t => t.DateTime > StartDate);
            transactions = transactions.Where(t => t.DateTime < EndDate);
            return transactions.OrderBy(t => t.DateTime);
        }

        private void LoadTotalExpenses()
        {
            DataPoints.Clear();
            //Add an empty data point to start at the origin.
            DataPoints.Add(new DataPoint());

            var filteredTransactions = GetFilteredTransactions();
            
            double total = 0;
            foreach (var transaction in filteredTransactions)
            {
                total += transaction.Amount;
                var dataPoint = new DataPoint
                {
                    X = transaction.DateTime,
                    Y = -total,
                };
                DataPoints.Add(dataPoint);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
