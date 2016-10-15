using Hazzik.Qif.Transactions;

namespace FinanceModel
{
    public class QuickenExpenseLoader : QuickenTransactionLoader
    {
        protected override bool ShouldInclude(BasicTransaction transaction)
        {
            //Only load expenses
            return true;//transaction.Amount < 0;
        }
    }
}
