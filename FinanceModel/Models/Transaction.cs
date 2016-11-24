using System;

namespace FinanceModel.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
    
        public string Description { get; set; }

        public double Amount { get; set; }
        
        public string Payee { get; set; }

        public bool Included { get; set; }
    }
    
}
