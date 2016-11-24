using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceModel;
using FinanceModel.Models;

namespace Finance.DataFilters
{
    public class DataFilterIncome : DataFilter
    {
        protected override bool ShouldInclude(Transaction transaction)
        {
            return transaction.Amount > 0 && transaction.Included;
        }
    }
}
