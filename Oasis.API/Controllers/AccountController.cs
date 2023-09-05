using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oasis.BL.DTOs.AccountDto;
using Oasis.BL.IServices;

namespace Oasis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;

        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }
        [HttpGet]
        public IActionResult Regiseration22(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto != null)
            {
               var token= accountServices.UserRegistration(userRegistrationDto);
                if (token != null) 
                {
                    return Ok(token);
                }
                else 
                { 
                    return BadRequest(); 
                }
            }
            return BadRequest();

        }


        [HttpPost]
        public async Task<IActionResult> Regiseration(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto != null)
            {
                var token = await accountServices.UserRegistration(userRegistrationDto);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }
            }

            return BadRequest();
        }
    }
}
