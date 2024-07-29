using AutoMapper;
using TopUpAPI.Dto;
using TopUpAPI.Models;
using TopUpAPI.Repositories.TopUpBeneficiaryRepository;

namespace TopUpAPI.Services.TopUpBeneficiaryService
{
    public class TopUpBeneficiaryService : ITopUpBeneficiaryService
    {
        private readonly IMapper _mapper;
        private readonly ITopUpBeneficiaryRepository _topUpBeneficiaryRepo;

        public TopUpBeneficiaryService(IMapper mapper, ITopUpBeneficiaryRepository topUpBeneficiaryRepo)
        {
            _mapper = mapper;
            _topUpBeneficiaryRepo = topUpBeneficiaryRepo;
        }
        public async Task<ResponseModel<GetTopUpBeneficiaryDto>> AddTopUpBeneficiaryAsync(AddTopUpBeneficiaryDto topUpBeneficiary)
        {
            try
            {
                var newTopUpBeneficiary = _mapper.Map<TopUpBeneficiary>(topUpBeneficiary);
                var addedTopUpBeneficiary = await _topUpBeneficiaryRepo.AddTopUpBeneficiary(newTopUpBeneficiary);
                var response = _mapper.Map<GetTopUpBeneficiaryDto>(addedTopUpBeneficiary);
                var successResponse = new ResponseModel<GetTopUpBeneficiaryDto>(
                    true,
                    200,
                    response,
                    "TopUpBeneficiary Added Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<GetTopUpBeneficiaryDto>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public async Task<ResponseModel<IEnumerable<TopUpBeneficiary>>> GetTopUpBeneficiariesAsync()
        {
            try
            {
                var listOfTopUpBeneficiary = await _topUpBeneficiaryRepo.GetTopUpBeneficiaries();
                var successResponse = new ResponseModel<IEnumerable<TopUpBeneficiary>>(
                    true,
                    200,
                    listOfTopUpBeneficiary,
                    "Top up beneficiary issued successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<IEnumerable<TopUpBeneficiary>>(false, 400, null, ex.Message);
                return errorRes;
            }
        }
    }
}