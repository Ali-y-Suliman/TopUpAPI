using TopUpAPI.Models;

namespace TopUpAPI.Repositories.TopUpBeneficiaryRepository
{
    public interface ITopUpBeneficiaryRepository
    {
        Task<TopUpBeneficiary> AddTopUpBeneficiary(TopUpBeneficiary topUpBeneficiary);
        Task<IEnumerable<TopUpBeneficiary>> GetTopUpBeneficiaries();
    }
}
