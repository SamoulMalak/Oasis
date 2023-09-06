using Oasis.BL.DTOs.AccountDto;
using Oasis.BL.DTOs.UserTokenInfo;


namespace Oasis.BL.IServices
{
    public interface IAccountServices
    {
        string CreateToken(UserTokenInfoDTO userTokenInfo);

        Task<string> UserRegistration(UserRegistrationDto userDto);
        Task<string> UserLogInAsync(UserLogInDto logInDto);

        Task<bool> CheckEmailIsExistsAsync(string Email);


    }
}
