// ExternalUserService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TopUpAPI.Services.UserBalanceService
{
    public class UserBalanceService : IUserBalanceService
    {
        private readonly HttpClient _httpClient;

        public UserBalanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreditUserBalanceAsync(string email, decimal amount)
        {
            await _httpClient.PostAsJsonAsync($"http://localhost:5195/api/UserBalance/credit/{email}", amount);
        }

        public async Task DebitUserBalanceAsync(string email, decimal amount)
        {
            await _httpClient.PostAsJsonAsync($"http://localhost:5195/api/UserBalance/debit/{email}", amount);
        }

        public async Task<decimal> GetUserBalanceByEmailAsync(string email)
        {
            var response = await _httpClient.GetStringAsync($"http://localhost:5195/api/UserBalance/{email}");
            if (decimal.TryParse(response, out var balance))
            {
                return balance;
            }
            return balance;
        }
    }
}
