using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TransactionsRepository
{
    public interface ITransactionsRepository
    {
        Task AddTransactions(Transactions transaction);
    }
}
