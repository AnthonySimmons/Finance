using FinanceModel;
using FinanceModel.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Finance.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public TransactionCollection Transactions => _dataModel.Transactions;        

        private DataModel _dataModel;

        public DataGridViewModel(DataModel dataModel)
        {
            _dataModel = dataModel;

            _dataModel.PropertyChanged += _dataModel_PropertyChanged;
        }

        private void _dataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //Transactions.Clear();
            //Transactions.AddRange(_dataModel.Transactions.OrderBy(t => t.Date));
        }

        
    }
}
