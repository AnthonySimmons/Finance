﻿using System;

namespace FinanceModel
{
    public class Transaction
    {
        public DateTime DateTime { get; set; }

        public TransactionType TransactionType { get; set; }
        
        public string Description { get; set; }

        public double Amount { get; set; }
        
    }

    public enum TransactionType
    {
        Unknown,
        Debit,
        Credit,
    }

    public class Expense : Transaction
    {

    }

    public class Deposit : Transaction
    {

    }
}
