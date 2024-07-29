using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TopUpOptionRepository
{
    public interface ITopUpOptionRepository
    {
        Task<TopUpOption> AddTopUpOption(TopUpOption topUpOption);
        Task<IEnumerable<TopUpOption>> GetTopUpOptions();
    }
}
