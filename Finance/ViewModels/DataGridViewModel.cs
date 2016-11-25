using FinanceModel;
using FinanceModel.Models;

namespace Finance.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public TransactionCollection Transactions => _dataModel.Transactions;        

        private DataModel _dataModel;

        public DataGridViewModel(DataModel dataModel)
        {
            _dataModel = dataModel;
        }
                        
    }
}
