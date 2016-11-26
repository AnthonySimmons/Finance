
using FinanceModel;
using FinanceModel.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Finance.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly LineChartViewModel _lineChartViewModel;
        public LineChartViewModel LineChartViewModel
        {
            get
            {
                return _lineChartViewModel;
            }
        }

        private readonly PieChartViewModel _pieChartViewModel;
        public PieChartViewModel PieChartViewModel
        {
            get
            {
                return _pieChartViewModel;
            }
        }

        private readonly DataGridViewModel _dataGridViewModel;
        public DataGridViewModel DataGridViewModel
        {
            get
            {
                return _dataGridViewModel;
            }
        }

        private readonly DataModel _dataModel;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            private set
            {
                if(value != _isLoading)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public ApplicationViewModel()
        {
            QuickenTransactionLoader loader = new QuickenTransactionLoader();
            QuickenTransactionSaver saver = new QuickenTransactionSaver(); 
            _dataModel = new DataModel(loader, saver);
            
            _lineChartViewModel = new LineChartViewModel(_dataModel);
            _pieChartViewModel = new PieChartViewModel(_dataModel);
            _dataGridViewModel = new DataGridViewModel(_dataModel);
        }
                
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadData(string[] fileNames)
        {
            IsLoading = true;
            await Task.Run(() =>
            {
                foreach (string fileName in fileNames)
                {
                    _dataModel.LoadDataFromFile(fileName);
                }
                _lineChartViewModel.ResetDates();
                _pieChartViewModel.ResetDates();
            });
            IsLoading = false;
        }

        public void SaveData(string fileName)
        {
            _dataModel.SaveDataToFile(fileName);
        }

        public void ClearData()
        {
            _dataModel.Clear();
        }

        public void RefreshData()
        {
            _dataModel.Refresh();
        }

    }
}
