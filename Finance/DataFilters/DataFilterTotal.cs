using FinanceModel.Models;

namespace Finance.DataFilters
{
    public class DataFilterTotal : DataFilter
    {
        protected override bool ShouldInclude(Transaction transaction)
        {
            return transaction.IsIncluded;
        }
    }
}
