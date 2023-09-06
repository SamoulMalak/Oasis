
namespace Oasis.BL.DTOs.AccountDto
{
    public class UserRegistrationDto :UserLogInDto
    {
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
