using EasyClinic.AuthService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using EasyClinic.AuthService.Domain.Exceptions;
using Hellang.Middleware.ProblemDetails;
using EasyClinic.AuthService.Infrastructure.Repository;
using EasyClinic.AuthService.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Globalization;
using EasyClinic.AuthService.Application.Commands.LoginUser;
using EasyClinic.AuthService.Application.Commands.RegisterUser;
using EasyClinic.AuthService.Domain.Contracts;
using KEmailSender;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication();

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) =>
        (builder.Environment.IsDevelopment() || builder.Environment.IsStaging()) 
        && ex is not HttpResponseCodeException;
    options.ShouldLogUnhandledException = (_, ex, d) => 
        (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
        && (d.Status is null or >= 400);
        options.Map<HttpResponseCodeException>(ex => new ProblemDetails() { 
        Title = ex.Title, Detail = ex.Message, Status = ex.Status, Type = ex.Type });
        options.MapToStatusCode<Exception>(500);
});

builder.Services.AddDbContext<IdentityServiceDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:IdentityServiceContainerConnection"])
);

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredLength = 6;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedAccount = false;
        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
        options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityServiceDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtOrigin:Issuer"],
            ValidAudience = builder.Configuration["JwtOrigin:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                builder.Configuration["JwtOrigin:Key"]!))
        };
    });

builder.Services.AddCors(options => {
    options.AddPolicy("ApiCorsPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:*", "https://localhost:*");
    });
});

builder.Services.AddFluentValidationAutoValidation(op => 
    op.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-GB");

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailPatternService, EmailPatternService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<LoginUserCommand>());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ApiCorsPolicy");

app.UseProblemDetails();

app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.SeedData<IdentityServiceDbContext>(app);

app.Run();
