using System;

namespace FinanceModel.Models
{
    public class Transaction
    {
        public DateTime DateTime { get; set; }

        public TransactionType TransactionType { get; set; }
        
        public string Description { get; set; }

        public double Amount { get; set; }
        
        public string Payee { get; set; }
    }


    public class Expense : Transaction
    {

    }

    public class Deposit : Transaction
    {

    }
}
