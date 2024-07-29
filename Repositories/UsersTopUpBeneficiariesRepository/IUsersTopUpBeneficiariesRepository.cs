using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.UsersTopUpBeneficiariesRepository
{
    public interface IUsersTopUpBeneficiariesRepository
    {
        Task<UsersTopUpBeneficiaries> AddUsersTopUpBeneficiaries(UsersTopUpBeneficiaries usersTopUpBeneficiaries);
        Task<IEnumerable<UsersTopUpBeneficiaries>> GetUsersTopUpBeneficiaries(int userId);
        Task<UsersTopUpBeneficiaries> UpdateUsersTopUpBeneficiariesAsync(UpdateUsersTopUpBeneficiariesDto usersTopUpBeneficiaries, int id);
    }
}
