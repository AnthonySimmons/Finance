using FinanceModel;
using FinanceModel.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Finance.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                if (_transactions != value)
                {
                    _transactions = value;
                    OnPropertyChanged(nameof(Transactions));
                }
            }
        }

        private DataModel _dataModel;

        public DataGridViewModel()
        {
            _dataModel = new DataModel(new QuickenTransactionLoader());

            _transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
        }

        public void LoadDataGridFromFile(params string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                _dataModel.LoadDataFromFile(filePath);
            }
            Transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
        }
    }
}
