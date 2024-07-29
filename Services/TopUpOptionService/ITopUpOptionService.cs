using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Services.TopUpOptionService
{
    public interface ITopUpOptionService
    {
        Task<ResponseModel<GetTopUpOptionDto>> AddTopUpOptionAsync(AddTopUpOptionDto topUpOption);
        Task<ResponseModel<IEnumerable<TopUpOption>>> GetTopUpOptionsAsync();
    }
}