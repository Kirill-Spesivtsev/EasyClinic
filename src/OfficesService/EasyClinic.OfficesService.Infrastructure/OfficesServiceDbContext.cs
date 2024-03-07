using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Infrastructure
{
    public class OfficesServiceDbContext : DbContext
    {
        public OfficesServiceDbContext(DbContextOptions<OfficesServiceDbContext> options) 
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

    }
}
