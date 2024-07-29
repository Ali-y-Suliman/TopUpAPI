using AutoMapper;
using TopUpAPI.Dto;
using TopUpAPI.Models;
using TopUpAPI.Repositories.TopUpOptionRepository;

namespace TopUpAPI.Services.TopUpOptionService
{
    public class TopUpOptionService : ITopUpOptionService
    {
        private readonly IMapper _mapper;
        private readonly ITopUpOptionRepository _topUpOptionRepo;

        public TopUpOptionService(IMapper mapper, ITopUpOptionRepository topUpOptionRepo)
        {
            _mapper = mapper;
            _topUpOptionRepo = topUpOptionRepo;
        }
        public async Task<ResponseModel<GetTopUpOptionDto>> AddTopUpOptionAsync(AddTopUpOptionDto topUpOption)
        {
            try
            {
                var newTopUpOption = _mapper.Map<TopUpOption>(topUpOption);
                var addedTopUpOption = await _topUpOptionRepo.AddTopUpOption(newTopUpOption);
                var response = _mapper.Map<GetTopUpOptionDto>(addedTopUpOption);
                var successResponse = new ResponseModel<GetTopUpOptionDto>(
                    true,
                    200,
                    response,
                    "TopUpOption Added Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<GetTopUpOptionDto>(false, 400, null, ex.Message);
                return errorRes;
            }
        }

        public async Task<ResponseModel<IEnumerable<TopUpOption>>> GetTopUpOptionsAsync()
        {
            try
            {
                var listOfTopUpOption = await _topUpOptionRepo.GetTopUpOptions();
                var successResponse = new ResponseModel<IEnumerable<TopUpOption>>(
                    true,
                    200,
                    listOfTopUpOption,
                    "TopUpBeneficiary issued Successfully"
                );
                return successResponse;
            }
            catch (Exception ex)
            {
                var errorRes = new ResponseModel<IEnumerable<TopUpOption>>(false, 400, null, ex.Message);
                return errorRes;
            }
        }
    }
}