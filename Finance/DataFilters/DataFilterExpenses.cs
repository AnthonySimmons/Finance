using FinanceModel.Models;

namespace Finance.DataFilters
{
    public class DataFilterExpenses : DataFilter
    {
        protected override bool ShouldInclude(Transaction transaction)
        {
            return transaction.Amount < 0 && transaction.Included;
        }
    }

}
