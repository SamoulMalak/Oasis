using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oasis.Data.Entities
{
    public class UserTable : IdentityUser
    {
        public int UserId { get; set; }
    }
}
