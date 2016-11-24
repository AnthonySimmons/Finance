
using FinanceModel;
using FinanceModel.Models;

namespace Finance.ViewModels
{
    public class ApplicationViewModel
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


        public ApplicationViewModel()
        {
            QuickenTransactionLoader loader = new QuickenTransactionLoader(); 
            _dataModel = new DataModel(loader);
            _lineChartViewModel = new LineChartViewModel(_dataModel);
            _pieChartViewModel = new PieChartViewModel(_dataModel);
            _dataGridViewModel = new DataGridViewModel(_dataModel);
            
        }

        public void LoadData(string[] fileNames)
        {
            foreach (string fileName in fileNames)
            {
                _dataModel.LoadDataFromFile(fileName);
            }
            _lineChartViewModel.ResetDates();
            _pieChartViewModel.ResetDates();
        }

    }
}
