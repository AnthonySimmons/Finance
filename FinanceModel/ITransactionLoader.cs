
namespace FinanceModel
{
    public interface ITransactionLoader
    {

        TransactionCollection Load(string filePath);
    }
}
