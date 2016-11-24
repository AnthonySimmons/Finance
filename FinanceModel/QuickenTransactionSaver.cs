
using FinanceModel.Models;
using Hazzik.Qif;
using Hazzik.Qif.Transactions;
using System;
using System.IO;

namespace FinanceModel
{
    public class QuickenTransactionSaver : ITransactionSaver
    {
        public bool Save(TransactionCollection transactions, string filePath)
        {
            bool success = true;
            try
            {
                QifDocument doc = GetQifDocument(transactions);

                using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate))
                {
                    doc.Save(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.Logs.Instance.Log(ex);
                success = false;
            }
            return success;
        }

        private QifDocument GetQifDocument(TransactionCollection transactions)
        {
            QifDocument qif = new QifDocument();
            foreach(Transaction transaction in transactions)
            {
                if (transaction is BankTransaction)
                {
                    qif.BankTransactions.Add(GetBasicTransaction(transaction));
                }
                else if (transaction is CreditCardTransaction)
                {
                    qif.CreditCardTransactions.Add(GetBasicTransaction(transaction));
                }                
            }
            return qif;
        }

        private BasicTransaction GetBasicTransaction(Transaction transaction)
        {
            return new BasicTransaction()
            {
                Amount = transaction.Amount,
                Date = transaction.Date,
                Memo = transaction.Description,
                Payee = transaction.Payee,
            };
        }

    }
}
