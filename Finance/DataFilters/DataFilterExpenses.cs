using FinanceModel;
using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.DataFilters
{
    public class DataFilterExpenses : DataFilter
    {
        protected override bool ShouldInclude(Transaction transaction)
        {
            return transaction.Amount < 0;
        }
    }

}
