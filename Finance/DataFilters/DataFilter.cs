using FinanceModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.DataFilters
{
    public abstract class DataFilter
    {
        public static string[] TransferDescriptions = new string[]
        {
            @"AUTHORIZED PMT BANK OF AMERI",
        };

        public virtual IEnumerable<DataPoint> Filter(IEnumerable<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            var filteredData = new List<DataPoint>();
            //Add an empty data point to start at the origin.
            filteredData.Add(new DataPoint());

            var filteredTransactions = FilterTransactions(transactions, startDate, endDate);

            ProcessTransactions(filteredTransactions, filteredData);
            return filteredData;
        }

        protected virtual IEnumerable<Transaction> FilterTransactions(IEnumerable<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            transactions = transactions.Where(t => ShouldInclude(t, startDate, endDate));
            return transactions.OrderBy(t => t.DateTime);
        }

        protected virtual bool ShouldInclude(Transaction transaction, DateTime startDate, DateTime endDate)
        {
            return !IsTransfer(transaction) && IsInDateRange(transaction.DateTime, startDate, endDate);
        }

        private bool IsTransfer(Transaction transaction)
        {
            return TransferDescriptions.Any(d => transaction.Description.Contains(d));
        }

        private bool IsInDateRange(DateTime source, DateTime startDate, DateTime endDate)
        {
            return source > startDate && source < endDate;
        }

        protected abstract void ProcessTransactions(IEnumerable<Transaction> transactions, IList<DataPoint> dataPoints);

    }
}


