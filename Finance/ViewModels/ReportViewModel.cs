using Finance.DataFilters;
using FinanceModel;
using FinanceModel.Models;
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

        private ReportType _reportType;
        public ReportType ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                if(_reportType != value)
                {
                    _reportType = value;
                    OnPropertyChanged(nameof(ReportType));
                    LoadDataPoints();
                }
            }
        }

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

        private static Dictionary<ReportType, Type> DataFilters = new Dictionary<ReportType, Type>
        {
            [ReportType.Unknown] = typeof(DataFilterTotal),
            [ReportType.Total] = typeof(DataFilterTotal),
            [ReportType.Expenses] = typeof(DataFilterExpenses),
            [ReportType.Income] = typeof(DataFilterIncome),
        };

        private DataFilter GetFilter(ReportType type)
        {
            return (DataFilter)Activator.CreateInstance(DataFilters[type]);
        }

        public ReportViewModel()
        {
            LoadTransactions();
            StartDate = new DateTime(2016, 10, 1);
            EndDate = DateTime.Now;
            ReportType = ReportType.Total;
        }
        

        private void LoadTransactions()
        {
            _dataModel = new ReportModel(new QuickenTransactionLoader());
            LoadDataFromFolder(Config.ExpensesDirectoryPath);
        }
        
        public void LoadDataFromFolder(params string [] filePaths)
        {
            _dataModel.Clear();
            foreach (var filepath in filePaths)
            {
                _dataModel.LoadDataFromFolder(filepath);
            }
            LoadDataPoints();
        }
                

        public void LoadDataFromFiles(params string[] filePaths)
        {
            _dataModel.Clear();
            foreach (var filepath in filePaths)
            {
                _dataModel.LoadDataFromFile(filepath);
            }
            LoadDataPoints();
        }

        private void LoadDataPoints()
        {
            DataFilter filter = GetFilter(ReportType);
            DataPoints.Clear();
            var dataPoints = filter.Filter(_dataModel.Transactions, StartDate, EndDate);
            foreach (var dataPoint in dataPoints)
            {
                DataPoints.Add(dataPoint);
            }
        }
     
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
