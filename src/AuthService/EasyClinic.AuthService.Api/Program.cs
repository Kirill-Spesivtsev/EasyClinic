using EasyClinic.AuthService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyClinic.AuthService.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

builder.Services.AddDbContext<IdentityServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityServiceConnection"])
);

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 6;
        
    })
    .AddEntityFrameworkStores<IdentityServiceDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
