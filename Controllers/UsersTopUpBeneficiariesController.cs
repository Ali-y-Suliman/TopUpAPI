using Microsoft.AspNetCore.Mvc;
using TopUpAPI.Dto;
using TopUpAPI.Models;
using TopUpAPI.Services.UsersTopUpBeneficiariesService;

namespace TopUpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersTopUpBeneficiariesController : ControllerBase
    {
        private readonly IUsersTopUpBeneficiariesService _usersTopUpBeneficiariesService;

        public UsersTopUpBeneficiariesController(IUsersTopUpBeneficiariesService usersTopUpBeneficiariesService)
        {
            _usersTopUpBeneficiariesService = usersTopUpBeneficiariesService;
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseModel<GetUsersTopUpBeneficiariesDto>>> AddUsersTopUpBeneficiaries(
            [FromBody] AddUsersTopUpBeneficiariesDto usersTopUpBeneficiary
        )
        {
            var response = await _usersTopUpBeneficiariesService.AddUsersTopUpBeneficiariesAsync(usersTopUpBeneficiary);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

       
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseModel<GetUsersTopUpBeneficiariesDto>>> GetUsersTopUpBeneficiaries(int userId)
        {
            var response = await _usersTopUpBeneficiariesService.GetUsersTopUpBeneficiariesAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<ResponseModel<GetUsersTopUpBeneficiariesDto>>> UpdateUsersTopUpBeneficiaries(
            [FromBody] UpdateUsersTopUpBeneficiariesDto usersTopUpBeneficiary,
            int id
        )
        {
            var response = await _usersTopUpBeneficiariesService.UpdateUsersTopUpBeneficiariesAsync(usersTopUpBeneficiary, id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}