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
            await _httpClient.PostAsJsonAsync($"userBalance/credit/{email}", amount);
        }

        public async Task DebitUserBalanceAsync(string email, decimal amount)
        {
            await _httpClient.PostAsJsonAsync($"userBalance/debit/{email}", amount);
        }

        public async Task<decimal> GetUserBalanceByEmailAsync(string email)
        {
            var response = await _httpClient.GetFromJsonAsync<UserBalanceResponse>($"userBalance/{email}");
            return response?.Balance ?? 0m;
        }

        private class UserBalanceResponse
        {
            public decimal Balance { get; set; }
        }
    }
}
