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

        public DataGridViewModel(DataModel dataModel)
        {
            _dataModel = dataModel;

            _dataModel.PropertyChanged += _dataModel_PropertyChanged;
            _transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
            _transactions.CollectionChanged += _transactions_CollectionChanged;
        }

        private void _dataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
        }

        private void _transactions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
        }
        
    }
}
