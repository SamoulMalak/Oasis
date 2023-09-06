using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oasis.BL.DTOs.AccountDto;
using Oasis.BL.DTOs.UserTokenInfo;
using Oasis.BL.IServices;
using Oasis.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Oasis.BL.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserTable> _userManager;
        private readonly SignInManager<UserTable> _signInManager;

        public AccountServices(IConfiguration configuration,
                               UserManager<UserTable> userManager,
                               SignInManager<UserTable> signInManager)
        {
             _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
     
        public async Task<bool> CheckEmailIsExistsAsync(string Email)
        {
            var result =await _userManager.FindByEmailAsync(Email);
            if (result == null) 
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public string CreateToken(UserTokenInfoDTO userTokenInfo)
        {

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,userTokenInfo.UserEmail),
            new Claim(ClaimTypes.Name, userTokenInfo.UserName)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       



        public async Task<string> UserLogInAsync(UserLogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
      
                var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
            if (result.Succeeded)
            {
                UserTokenInfoDTO userTokenInfo = new UserTokenInfoDTO
                {
                    UserName = user.UserName,
                    UserEmail = user.Email
                };
                string token = CreateToken(userTokenInfo);


                return token;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UserRegistration(UserRegistrationDto userDto)
        {
            var user = new UserTable
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };
       
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    UserLogInDto userLogIn = new UserLogInDto
                    {
                        Email = userDto.Email,
                        Password = userDto.Password
                    };
                    var token = await UserLogInAsync(userLogIn);
                    return token;
                }
  
          
            return string.Empty;
        }

     
    }
}
