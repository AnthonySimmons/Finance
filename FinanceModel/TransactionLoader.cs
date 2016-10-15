using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hazzik;
using Hazzik.Qif;

namespace FinanceModel
{
    public class TransactionLoader : ITransactionLoader
    {
        public IEnumerable<Transaction> Load(string filePath)
        {
            QifDocument doc = GetQif(filePath);

            return GetExpenseModels(doc);
        }
        
        private QifDocument GetQif(string filePath)
        {
            QifDocument doc;
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                doc = QifDocument.Load(stream);
            }
            return doc;
        }

        private IEnumerable<Transaction> GetExpenseModels(QifDocument doc)
        {
            var expenses = new List<Transaction>();
            foreach(var transaction in doc.BankTransactions)
            {
                TransactionType type;
                Enum.TryParse(transaction.Number, out type);

                var expense = new Transaction
                {
                    Amount = (double)transaction.Amount,
                    DateTime = transaction.Date,
                    Description = transaction.Memo,
                    TransactionType = type,
                };
                expenses.Add(expense);
            }
            return expenses;
        }
    }
}
