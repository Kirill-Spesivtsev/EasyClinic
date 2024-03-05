using EasyClinic.AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;

namespace EasyClinic.AuthService.Infrastructure
{
    public class IdentityServiceDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public IdentityServiceDbContext(DbContextOptions<IdentityServiceDbContext> options) 
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

    }
}