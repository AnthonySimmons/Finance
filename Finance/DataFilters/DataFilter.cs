using FinanceModel;
using FinanceModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.DataFilters
{
    public abstract class DataFilter
    {
        public static string[] TransferDescriptions = new string[]
        {
            @"TRANSFER",
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
            return TransferDescriptions.Any(d => transaction.Description.Contains(d) || transaction.Payee.Contains(d));
        }

        private bool IsInDateRange(DateTime source, DateTime startDate, DateTime endDate)
        {
            return source > startDate && source < endDate;
        }

        protected abstract bool ShouldInclude(Transaction transaction);

        protected virtual void ProcessTransactions(IEnumerable<Transaction> transactions, IList<DataPoint> dataPoints)
        {
            double total = 0;
            foreach (var transaction in transactions)
            {
                if (!ShouldInclude(transaction))
                {
                    continue;
                }

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


