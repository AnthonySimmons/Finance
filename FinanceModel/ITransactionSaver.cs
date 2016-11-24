
namespace FinanceModel
{
    public interface ITransactionSaver
    {
        bool Save(TransactionCollection transactions, string filePath);
    }
}
