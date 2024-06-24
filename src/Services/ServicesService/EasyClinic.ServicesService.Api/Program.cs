using EasyClinic.ServicesService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Api.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using EasyClinic.ServicesService.Domain.Exceptions;
using Hellang.Middleware.ProblemDetails;
using EasyClinic.ServicesService.Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Globalization;
using EasyClinic.ServicesService.Application.Commands;
using EasyClinic.ServicesService.Domain.Contracts;
using Serilog;
using System.Reflection;
using MassTransit;
using EasyClinic.ServicesService.Application.Queries;
using EasyClinic.ServicesService.Application.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Services API",
        Description = "EasyClinic Web API Application",
        Contact = new OpenApiContact
        {
            Name = "Contacts",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

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
        && (d.Status is null or >= 500);
    options.Map<HttpResponseCodeException>(ex => new ProblemDetails() { 
        Title = ex.Title, Detail = ex.Message, Status = ex.Status, Type = ex.Type });
      options.MapToStatusCode<Exception>(500);
});

builder.Services.AddDbContext<ServicesServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ServicesServiceConnection"])
);

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

builder.Services.AddMassTransit(m =>
{
    m.AddConsumers(Assembly.GetAssembly(typeof(CreateServiceCommand)));
    m.UsingAzureServiceBus((context, config) =>
    {
        config.Host(builder.Configuration["ConnectionStrings:AzureServiceBusConnection"]!);

        config.ConfigureEndpoints(context);
    });
    
});

builder.Host.UseSerilog(LoggerConfig.ConfigureLogger);

builder.Services.AddFluentValidationAutoValidation(op => 
    op.DisableDataAnnotationsValidation = true)
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateServiceCommand>();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-GB");

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IServicesRepository, ServicesRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateServiceCommand>());

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("ApiCorsPolicy");

app.UseProblemDetails();

app.UseAuthorization();

app.MapControllers();

await DatabaseSeeder.SeedData(app);

app.Run();



