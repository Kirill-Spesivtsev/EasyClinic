using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Domain.Entities
{
    /// <summary>
    /// Represents an AspNetUsers table in database.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Creation time of the user.
        /// </summary>
        public DateTimeOffset CreatedTime { get;set; } = DateTimeOffset.Now;
    }
}
