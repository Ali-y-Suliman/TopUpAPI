using Microsoft.AspNetCore.Mvc;
using TopUpAPI.Dto;
using TopUpAPI.Services.TopUpBeneficiaryService;
using TopUpAPI.Models;

namespace TopUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpBeneficiaryController : ControllerBase
    {
        private readonly ITopUpBeneficiaryService _topUpBeneficiaryService;

        public TopUpBeneficiaryController(ITopUpBeneficiaryService topUpBeneficiaryService)
        {
            _topUpBeneficiaryService = topUpBeneficiaryService;
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseModel<GetTopUpBeneficiaryDto>>> AddTopUpBeneficiary(
            [FromBody] AddTopUpBeneficiaryDto topUpBeneficiary
        )
        {
            var response = await _topUpBeneficiaryService.AddTopUpBeneficiaryAsync(topUpBeneficiary);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<ResponseModel<IEnumerable<GetTopUpBeneficiaryDto>>>> GetTopUpBeneficiaries()
        {
            var response = await _topUpBeneficiaryService.GetTopUpBeneficiariesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok();
        }

    }
}