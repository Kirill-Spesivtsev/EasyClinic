
using EasyClinic.OfficesService.Api.Helpers;
using EasyClinic.OfficesService.Domain.Exceptions;
using EasyClinic.OfficesService.Domain.RepositoryContracts;
using EasyClinic.OfficesService.Infrastructure;
using EasyClinic.OfficesService.Infrastructure.Repository;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using EasyClinic.OfficesService.Application.Queries.GetAllOffices;
using Microsoft.Extensions.FileProviders;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using EasyClinic.OfficesService.Application.Commands.ChangeOfficeStatus;
using EasyClinic.OfficesService.Application.Commands.CreateOffice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation(op => 
    op.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOfficeCommandValidator>();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-GB");

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

builder.Services.AddDbContext<OfficesServiceDbContext>(options =>
	options.UseCosmos(
    	builder.Configuration["ConnectionStrings-OfficesServiceAzureCosmosDbConnection"]!,
        "ToDoList"
    ));

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllOfficesQuery>());

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var vaultUri = builder.Configuration["Azure:KeyVault:KeyVaultUri"]!;
var clientId = builder.Configuration["Azure:KeyVault:ClientId"]!;
var clientSecret = builder.Configuration["Azure:KeyVault:ClientSecret"];
var tenantId = builder.Configuration["Azure:KeyVault:TenantId"];
var credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);
var secretClient = new SecretClient(new Uri(vaultUri), credentials);
builder.Configuration.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseProblemDetails();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

await DbInitializer.Initialize<OfficesServiceDbContext>(app);

app.Run();
