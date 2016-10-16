using FinanceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.DataFilters
{
    public class DataFilterTotal : DataFilter
    {
        protected override void ProcessTransactions(IEnumerable<Transaction> transactions, IList<DataPoint> dataPoints)
        {
            double total = 0;
            foreach (var transaction in transactions)
            {
                total += transaction.Amount;
                var dataPoint = new DataPoint
                {
                    X = transaction.DateTime,
                    Y = total,
                };
                dataPoints.Add(dataPoint);
            }
        }
    }
}
