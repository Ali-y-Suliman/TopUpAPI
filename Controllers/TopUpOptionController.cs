using Microsoft.AspNetCore.Mvc;
using TopUpAPI.Dto;
using TopUpAPI.Services.TopUpOptionService;
using TopUpAPI.Models;

namespace TopUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopUpOptionController : ControllerBase
    {
        private readonly ITopUpOptionService _topUpOptionService;

        public TopUpOptionController(ITopUpOptionService TopUpOptionService)
        {
            _topUpOptionService = TopUpOptionService;
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseModel<GetTopUpOptionDto>>> AddTopUpOption(
            [FromBody] AddTopUpOptionDto topUpOption
        )
        {
            var response = await _topUpOptionService.AddTopUpOptionAsync(topUpOption);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet()]
        public async Task<ActionResult<ResponseModel<IEnumerable<GetTopUpOptionDto>>>> GetTopUpBeneficiaries()
        {
            var response = await _topUpOptionService.GetTopUpOptionsAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}