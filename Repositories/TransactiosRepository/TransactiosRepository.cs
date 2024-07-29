using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TransactionsRepository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly DataContext _context;

        public TransactionsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddTransactions(Transactions transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
