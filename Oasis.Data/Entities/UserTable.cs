using Microsoft.AspNetCore.Identity;


namespace Oasis.Data.Entities
{
    public class UserTable : IdentityUser
    {
        public int UserId { get; set; }
    }
}
