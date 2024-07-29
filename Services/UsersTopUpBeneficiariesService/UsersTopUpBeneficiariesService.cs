using AutoMapper;
using TopUpAPI.Dto;
using TopUpAPI.Models;
using TopUpAPI.Repositories.TransactionsRepository;
using TopUpAPI.Repositories.UsersTopUpBeneficiariesRepository;
using TopUpAPI.Services.UserBalanceService;
using TopUpAPI.Services.UserService;

namespace TopUpAPI.Services.UsersTopUpBeneficiariesService
{
    public class UsersTopUpBeneficiariesService : IUsersTopUpBeneficiariesService
    {
        private readonly IMapper _mapper;
        private readonly IUsersTopUpBeneficiariesRepository _usersTopUpBeneficiariesRepo;
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IUserBalanceService _userBalanceService;
        private readonly IUserService _userService;

        public UsersTopUpBeneficiariesService(
            IMapper mapper,
            IUsersTopUpBeneficiariesRepository usersTopUpBeneficiariesRepo,
            ITransactionsRepository transactionsRepo,
            IUserBalanceService userBalanceService,
            IUserService userService
            )
        {
            _mapper = mapper;
            _usersTopUpBeneficiariesRepo = usersTopUpBeneficiariesRepo;
            _transactionsRepo = transactionsRepo;
            _userBalanceService = userBalanceService;
            _userService = userService;
        }
        public async Task<ResponseModel<GetUsersTopUpBeneficiariesDto>> AddUsersTopUpBeneficiariesAsync(AddUsersTopUpBeneficiariesDto usersTopUpBeneficiary)
        {
            try
            {
                decimal topUpAmount = usersTopUpBeneficiary.TopUpAmount;
                var newUsersTopUpBeneficiaries = _mapper.Map<UsersTopUpBeneficiaries>(usersTopUpBeneficiary);
                
                var user = await _userService.GetUserAsync(newUsersTopUpBeneficiaries.UserId);
                if(user.Data == null){
                    throw new Exception("User not found");
                }

                // call check method
                int utbId = await CheckTopUpAbility(user.Data, newUsersTopUpBeneficiaries, topUpAmount);

                //#2 Debit
                await _userBalanceService.DebitUserBalanceAsync(user.Data.Email, topUpAmount + 1);

                AddTransactionDto transaction = new AddTransactionDto {
                    TopUpAmount = topUpAmount,
                    TopUpFeeAmount = 1,
                    TopUpTotalAmount = topUpAmount + 1,
                    TransactionDate = DateTime.UtcNow
                };

                // check if utb is exist so we shouldn't add it again
                if(utbId < 0){
                    UsersTopUpBeneficiaries utb = await _usersTopUpBeneficiariesRepo.AddUsersTopUpBeneficiaries(newUsersTopUpBeneficiaries);
                    _mapper.Map<GetUsersTopUpBeneficiariesDto>(utb);
                    transaction.UsersTopUpBeneficiariesId = utb.Id;
                } else {
                    transaction.UsersTopUpBeneficiariesId = utbId;
                }

                //#3 Add the transaction
                await _transactionsRepo.AddTransactions(_mapper.Map<Transactions>(transaction));
                
                return new ResponseModel<GetUsersTopUpBeneficiariesDto>(
                        true,
                        200,
                        null,
                        "TopUp action Succeed"
                    );
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<GetUsersTopUpBeneficiariesDto>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public async Task<int> CheckTopUpAbility(User user, UsersTopUpBeneficiaries newUsersTopUpBeneficiaries, decimal topUpAmount) {
            ResponseModel<IEnumerable<UsersTopUpBeneficiaries>> usersTopUpBeneficiaries = await GetUsersTopUpBeneficiariesAsync(newUsersTopUpBeneficiaries.UserId);
                
            int existingUsersBeneficiaryId = -1;

            if(user == null){
                throw new Exception("User not found");
            }

            decimal userBalance = await _userBalanceService.GetUserBalanceByEmailAsync(user.Email);

            if(usersTopUpBeneficiaries.Data != null){
                int totalActiveBeneficiaries = usersTopUpBeneficiaries.Data.Where(utb => utb.IsActive).Sum(utb => 1);
                
                //Check Maximum of 5 active top-up beneficiaries
                if(totalActiveBeneficiaries == 5) {
                    throw new Exception("You exceed your maximum of 5 active top-up beneficiaries."); 
                }

                var usersTopUpBeneficiary = usersTopUpBeneficiaries.Data.Where(utb => utb.TopUpBeneficiaryId == newUsersTopUpBeneficiaries.TopUpBeneficiaryId).FirstOrDefault();
                
                if(usersTopUpBeneficiary != null){
                    existingUsersBeneficiaryId = usersTopUpBeneficiary.Id;
                }
                decimal _monthlyTotalTransactionsForBeneficiary = GetMonthlyTopUpTransactionsAsync(usersTopUpBeneficiaries.Data, newUsersTopUpBeneficiaries.TopUpBeneficiaryId);
                
                //Check monthly limit for verified User
                if(user != null && user.IsVerified){
                    if (_monthlyTotalTransactionsForBeneficiary + topUpAmount > 500){
                        throw new Exception("You exceed your monthly limit"); 
                    }
                //Check monthly limit for unverified User
                } else {
                    if (_monthlyTotalTransactionsForBeneficiary + topUpAmount > 1000){
                        throw new Exception("You exceed your monthly limit"); 
                    }
                }

                //Check monthly limit for all beneficiaries
                decimal _monthlyTotalTransactions = GetMonthlyTopUpTransactionsAsync(usersTopUpBeneficiaries.Data);
                if(_monthlyTotalTransactions + topUpAmount > 3000){
                        throw new Exception("You exceed your monthly limit"); 
                }
            }

            //Check the balance > topUpAmount + 1
            if(userBalance < topUpAmount + 1){
                throw new Exception("You don't have enough balance");
            }
            
            return existingUsersBeneficiaryId;
        }

        public async Task<ResponseModel<IEnumerable<UsersTopUpBeneficiaries>>> GetUsersTopUpBeneficiariesAsync(int userId)
        {
            try
            {
                var listOfTopUpBeneficiary = await _usersTopUpBeneficiariesRepo.GetUsersTopUpBeneficiaries(userId);
                var successResponse = new ResponseModel<IEnumerable<UsersTopUpBeneficiaries>>(
                    true,
                    200,
                    listOfTopUpBeneficiary,
                    "UsersTopUpBeneficiaries issued successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<IEnumerable<UsersTopUpBeneficiaries>>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public decimal GetMonthlyTopUpTransactionsAsync(IEnumerable<UsersTopUpBeneficiaries> usersTopUpBeneficiaries, int? beneficiaryId = null)
        {
            try
            {
                DateTime currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                decimal _totalUserTransactions = 0;
                if(beneficiaryId == null){
                    var beneficiary = usersTopUpBeneficiaries.FirstOrDefault(p => p.TopUpBeneficiaryId == beneficiaryId);
                    if (beneficiary != null && beneficiary.Transactions != null)
                    {
                        _totalUserTransactions = beneficiary.Transactions
                            .Where(t => t.TransactionDate >= currentMonth && t.TransactionDate < currentMonth.AddMonths(1))
                            .Sum(t => t.TopUpAmount);
                        return _totalUserTransactions;
                    }
                    else
                    {
                        return 0;
                    }
                } else {
                    _totalUserTransactions = usersTopUpBeneficiaries
                    .SelectMany(usersTopUpBeneficiary => usersTopUpBeneficiary?.Transactions ?? new List<Transactions>())
                    .Where(t => t.TransactionDate >= currentMonth && t.TransactionDate < currentMonth.AddMonths(1))
                    .Sum(t => t?.TopUpAmount ?? 0);
                }
                return _totalUserTransactions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResponseModel<UsersTopUpBeneficiaries>> UpdateUsersTopUpBeneficiariesAsync(UpdateUsersTopUpBeneficiariesDto usersTopUpBeneficiaries, int id)
        {
            try
            {
                var addedUsersTopUpBeneficiaries = await _usersTopUpBeneficiariesRepo.UpdateUsersTopUpBeneficiariesAsync(usersTopUpBeneficiaries, id);
                var successResponse = new ResponseModel<UsersTopUpBeneficiaries>(
                    true,
                    200,
                    addedUsersTopUpBeneficiaries,
                    "UsersTopUpBeneficiaries Updated Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<UsersTopUpBeneficiaries>(false, 400, null, ex.Message);
                return errorRes;
            }
        }
    }
}