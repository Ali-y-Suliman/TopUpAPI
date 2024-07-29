using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Services.UserBalanceService
{
    public interface IUserBalanceService
    {
        Task<decimal> GetUserBalanceByEmailAsync(string email);
        Task CreditUserBalanceAsync(string email, decimal amount);
        Task DebitUserBalanceAsync(string email, decimal amount);
    }
}