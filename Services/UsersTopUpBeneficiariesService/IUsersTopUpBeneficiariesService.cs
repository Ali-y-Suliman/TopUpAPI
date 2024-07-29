using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Services.UsersTopUpBeneficiariesService
{
    public interface IUsersTopUpBeneficiariesService
    {
        Task<ResponseModel<GetUsersTopUpBeneficiariesDto>> AddUsersTopUpBeneficiariesAsync(AddUsersTopUpBeneficiariesDto usersTopUpBeneficiaries);
        Task<ResponseModel<IEnumerable<UsersTopUpBeneficiaries>>> GetUsersTopUpBeneficiariesAsync(int userId);
        decimal GetMonthlyTopUpTransactionsAsync(IEnumerable<UsersTopUpBeneficiaries> usersTopUpBeneficiaries, int? beneficiaryId = null);
        Task<ResponseModel<UsersTopUpBeneficiaries>> UpdateUsersTopUpBeneficiariesAsync(UpdateUsersTopUpBeneficiariesDto usersTopUpBeneficiaries, int id);
    }
}