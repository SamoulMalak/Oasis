using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.BL.DTOs.AccountDto
{
    public class UserRegistrationDto :UserLogInDto
    {
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
