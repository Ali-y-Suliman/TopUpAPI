using AutoMapper;
using TopUpAPI.Dto;
using TopUpAPI.Models;
using TopUpAPI.Repositories.UserRepository;

namespace TopUpAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepo;

        public UserService(IMapper mapper, IUserRepository userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        public async Task<ResponseModel<GetUserDto>> AddUserAsync(AddUserDto user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);
                var addedUser = await _userRepo.AddUser(newUser);
                var response = _mapper.Map<GetUserDto>(addedUser);
                var successResponse = new ResponseModel<GetUserDto>(
                    true,
                    200,
                    response,
                    "User Added Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<GetUserDto>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public async Task<ResponseModel<User>> GetUserAsync(int id)
        {
            try
            {
                var user = await _userRepo.GetUser(id);
                var successResponse = new ResponseModel<User>(
                    true,
                    200,
                    user,
                    "TopUpBeneficiary issued Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<User>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public async Task<ResponseModel<User>> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepo.GetUserByEmail(email);
                var successResponse = new ResponseModel<User>(
                    true,
                    200,
                    user,
                    "TopUpBeneficiary issued Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<User>(false, 400, null, ex.Message);
                return errorRes;
            }
        }
    }
}