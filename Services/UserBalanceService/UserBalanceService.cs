// ExternalUserService.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TopUpAPI.Services.UserBalanceService
{
    public class UserBalanceService : IUserBalanceService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserBalanceService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task CreditUserBalanceAsync(string email, decimal amount)
        {
            var client = _httpClientFactory.CreateClient("balanceBaseUrl");
            await client.PostAsJsonAsync($"credit/{email}", amount);
        }

        public async Task DebitUserBalanceAsync(string email, decimal amount)
        {
            var client = _httpClientFactory.CreateClient("balanceBaseUrl");
            await client.PostAsJsonAsync($"debit/{email}", amount);
        }

        public async Task<decimal> GetUserBalanceByEmailAsync(string email)
        {
            var client = _httpClientFactory.CreateClient("balanceBaseUrl");
          
            var response = await client.GetStringAsync($"{email}");
            if (decimal.TryParse(response, out var balance))
            {
                return balance;
            }
            return balance;
        }
    }
}
