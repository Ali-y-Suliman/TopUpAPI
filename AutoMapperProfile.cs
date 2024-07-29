using AutoMapper;
using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace Users
{
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<AddUserDto, User>();
            CreateMap<AddUsersTopUpBeneficiariesDto, UsersTopUpBeneficiaries>();
            CreateMap<UsersTopUpBeneficiaries, GetUsersTopUpBeneficiariesDto>();
            CreateMap<UpdateUsersTopUpBeneficiariesDto, UsersTopUpBeneficiaries>();
            CreateMap<TopUpBeneficiary, GetTopUpBeneficiaryDto>();
            CreateMap<AddTopUpBeneficiaryDto, TopUpBeneficiary>();
            CreateMap<TopUpOption, GetTopUpOptionDto>();
            CreateMap<AddTopUpOptionDto, TopUpOption>();
            CreateMap<AddTransactionDto, Transactions>();
            CreateMap<Transactions, GetTransactionDto>();

        }
    }
}