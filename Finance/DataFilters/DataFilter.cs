using Finance.ViewModels;
using FinanceModel;
using FinanceModel.Models;
using FinanceModel.Transfers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.DataFilters
{
    public abstract class DataFilter
    {
        public virtual IEnumerable<T> Filter<T>(IEnumerable<Transaction> transactions, DateTime startDate, DateTime endDate)  where T : DataPoint, new()
        {
            var filteredData = new List<T>();

            var filteredTransactions = FilterTransactions(transactions, startDate, endDate);

            ProcessTransactions<T>(filteredTransactions, filteredData);
            return filteredData;
        }

        protected virtual IEnumerable<Transaction> FilterTransactions(IEnumerable<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            transactions = transactions.Where(t => ShouldInclude(t, startDate, endDate));
            return transactions.OrderBy(t => t.Date);
        }

        protected virtual bool ShouldInclude(Transaction transaction, DateTime startDate, DateTime endDate)
        {
            return !TransferManager.IsTransfer(transaction) && IsInDateRange(transaction.Date, startDate, endDate) && transaction.Included;
        }
       
        private bool IsInDateRange(DateTime source, DateTime startDate, DateTime endDate)
        {
            return source > startDate && source < endDate;
        }

        protected abstract bool ShouldInclude(Transaction transaction);

        protected virtual void ProcessTransactions<T>(IEnumerable<Transaction> transactions, IList<T> dataPoints) where T : DataPoint
        {
            decimal total = 0;
            foreach (var transaction in transactions)
            {
                if (!ShouldInclude(transaction))
                {
                    continue;
                }

                total += transaction.Amount;
                var dataPoint = DataPointFactory.GetDataPoint(typeof(T), transaction, total) as T;

                dataPoints.Add(dataPoint);
            }
        }

    }
}


