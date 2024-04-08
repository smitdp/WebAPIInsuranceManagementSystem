using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIInsuranceManagementSystem.Services.DTOs;
using WebAPIInsuranceManagementSystem.Services.Services.IServices;


namespace WebAPIInsuranceManagementSystem.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("buy-policy")]
        public async Task<IActionResult> BuyPolicy(UserPolicyDTO userPolicyDTO)
        {
            try
            {
                if (userPolicyDTO == null)
                {
                    return BadRequest("Request body is null");
                }

                int statusCode = await _userService.BuyPolicy(userPolicyDTO);

                switch (statusCode)
                {
                    case 1:
                        return Ok("Policy purchased successfully");
                    case -1:
                        return BadRequest("Request is null");
                    default:
                        return StatusCode(500, "An error occurred while processing your request");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("my-policies/{userId}")]
        public async Task<IActionResult> GetBoughtPolicies(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID");
                }

                var myPolicies = await _userService.GetBoughtPolicies(userId);

                if (myPolicies == null || !myPolicies.Any())
                {
                    return NotFound("No policy found");
                }

                return Ok(myPolicies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("agents")]
        public async Task<ActionResult<List<UserDTO>>> GetAgents()
        {
            try
            {
                List<UserDTO> agents = await _userService.GetApprovedAgents();
                if(agents == null || !agents.Any())
                {
                    return NotFound();
                }


                return Ok(agents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving agents: {ex.Message}");
            }
        }

        [HttpGet("user-info/{userId}")]
        public async Task<ActionResult<UserDTO>> GetUserInfo(int userId)
        {
            UserDTO user = await _userService.GetUserInfo(userId);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        } 


    }
}
