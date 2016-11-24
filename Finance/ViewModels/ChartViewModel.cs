using Finance.DataFilters;
using FinanceModel;
using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Finance.ViewModels
{
    public abstract class ChartViewModel : ViewModelBase
    {
        protected DataModel _dataModel;


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
                    LoadDataPoints();
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
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                    LoadDataPoints();
                }
            }
        }
        
        private ReportType _reportType;
        public ReportType ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                if (_reportType != value)
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



        public ChartViewModel()
        {
            _dataModel = new DataModel(new QuickenTransactionLoader());
            
            StartDate = _dataModel.GetEarliestTransaction();
            EndDate = _dataModel.GetLatestTransaction();
            ReportType = ReportType.Total;
        }

        protected abstract void LoadDataPoints();
                
        public void LoadDataFromFiles(params string[] filePaths)
        {
            _dataModel.Clear();
            foreach (var filepath in filePaths)
            {
                _dataModel.LoadDataFromFile(filepath);
                StartDate = _dataModel.GetEarliestTransaction();
                EndDate = _dataModel.GetLatestTransaction();
            }
            LoadDataPoints();
        }


        protected static Dictionary<ReportType, Type> DataFilters = new Dictionary<ReportType, Type>
        {
            [ReportType.Unknown] = typeof(DataFilterTotal),
            [ReportType.Total] = typeof(DataFilterTotal),
            [ReportType.Expenses] = typeof(DataFilterExpenses),
            [ReportType.Income] = typeof(DataFilterIncome),
        };

        protected virtual DataFilter GetFilter(ReportType type)
        {
            return (DataFilter)Activator.CreateInstance(DataFilters[type]);
        }

    }
}
