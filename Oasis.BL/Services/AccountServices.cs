using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Oasis.BL.DTOs.AccountDto;
using Oasis.BL.DTOs.UserTokenInfo;
using Oasis.BL.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountServices(IConfiguration configuration,
                               UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        /// <summary>
        /// this function to check if email is exist or not 
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>true if this email is </returns>
        public async Task<bool> CheckEmailIsExistsAsync(string Email)
        {
            var result =await userManager.FindByEmailAsync(Email);
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
            try
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,userTokenInfo.UserEmail),
            new Claim(ClaimTypes.Name, userTokenInfo.UserName)
        };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = creds,
                    Issuer = configuration["Jwt:Issuer"]
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your requirement
                return null;
            }
        }

        //public string CreateToken(UserTokenInfoDTO userTokenInfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier,userTokenInfo.UserName),
        //        new Claim(ClaimTypes.Email,userTokenInfo.UserEmail)
        //    };
        //    var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
        //        configuration["Jwt:Audience"],
        //        claims,
        //        expires: DateTime.Now.AddHours(1),
        //        signingCredentials: credentials);




        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public async Task<string> UserLogInAsync(UserLogInDto logInDto)
        {
            var user = await userManager.FindByEmailAsync(logInDto.Email);
      
                var result = await signInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
                UserTokenInfoDTO userTokenInfo = new UserTokenInfoDTO
                {
                    UserName = user.UserName,
                    UserEmail = user.Email
                };
               string token = CreateToken(userTokenInfo);
            return token;
           

        }

        public async Task<string> UserRegistration(UserRegistrationDto userDto)
        {
            var user = new IdentityUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };
            try
            {
                var result = await userManager.CreateAsync(user, userDto.Password);
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
            }
            catch (Exception ex) 
            {
                var x =ex.Message;
            }
          
            return string.Empty;
        }

     
    }
}
