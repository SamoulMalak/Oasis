﻿using Microsoft.AspNetCore.Mvc;
using Oasis.BL.DTOs.AccountDto;
using Oasis.BL.IServices;

namespace Oasis.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }


        [HttpPost("regiseration")]
        public async Task<IActionResult> Regiseration(UserRegistrationDto userRegistrationDto)
        {
            if (userRegistrationDto != null)
            {
                if(await _accountServices.CheckEmailIsExistsAsync(userRegistrationDto.Email)) 
                {
                    return BadRequest("This Eamil is already used");
                }
                var token = await _accountServices.UserRegistration(userRegistrationDto);
                 if (!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }
            }

            return BadRequest();
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(UserLogInDto userLogIn)
        {
            if (userLogIn != null)
            {
                string token = await _accountServices.UserLogInAsync(userLogIn);
                if (!string.IsNullOrEmpty(token))
                {
                    return Ok(token);
                }
                return Unauthorized();
            }
            return Unauthorized();
        }
    }
}
