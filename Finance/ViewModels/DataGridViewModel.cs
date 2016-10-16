using FinanceModel;
using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if(_transactions != value)
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
            _dataModel.LoadDataFromFolder(Config.ExpensesDirectoryPath);
            _transactions = new ObservableCollection<Transaction>(_dataModel.Transactions.OrderBy(t => t.Date));
        }


    }
}
