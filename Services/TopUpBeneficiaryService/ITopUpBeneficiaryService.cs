using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Services.TopUpBeneficiaryService
{
    public interface ITopUpBeneficiaryService
    {
        Task<ResponseModel<GetTopUpBeneficiaryDto>> AddTopUpBeneficiaryAsync(AddTopUpBeneficiaryDto topUpBeneficiary);
        Task<ResponseModel<IEnumerable<TopUpBeneficiary>>> GetTopUpBeneficiariesAsync();
    }
}