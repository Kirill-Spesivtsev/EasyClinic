using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset CreatedTime { get;set; } = DateTimeOffset.Now;
    }
}
