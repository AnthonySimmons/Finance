using System;
using System.IO;
using System.Collections.Generic;
using Hazzik.Qif;
using Hazzik.Qif.Transactions;

namespace FinanceModel
{
    public class QuickenTransactionLoader : ITransactionLoader
    {
        public TransactionCollection Load(string filePath)
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

        protected TransactionCollection GetExpenseModels(QifDocument doc)
        {
            var expenses = new TransactionCollection();
            double total = 0;

            foreach(var transaction in doc.BankTransactions)
            {
                if(!ShouldInclude(transaction))
                {
                    continue;
                }

                TransactionType type;
                Enum.TryParse(transaction.Number, out type);

                double amount = (double)transaction.Amount;
                total += amount;
                expenses.Add(transaction.Memo, amount, transaction.Date, type);
                
            }
            return expenses;
        }

        protected virtual bool ShouldInclude(BasicTransaction transaction)
        {
            return true;
        }
    }

}
