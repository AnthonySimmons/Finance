using FinanceModel;
using FinanceModel.Models;
using System.Collections.ObjectModel;

namespace Finance.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {        
        public ObservableCollection<Transaction> Transactions => new ObservableCollection<Transaction>(_dataModel.Transactions);        

        private DataModel _dataModel;

        public DataGridViewModel(DataModel dataModel)
        {
            _dataModel = dataModel;
            _dataModel.PropertyChanged += _dataModel_PropertyChanged;
        }

        private void _dataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Transactions));
        }
    }
}
