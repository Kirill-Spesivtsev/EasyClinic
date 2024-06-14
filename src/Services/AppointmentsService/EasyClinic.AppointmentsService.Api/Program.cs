using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Globalization;
using EasyClinic.AppointmentsService.Infrastructure;
using EasyClinic.AppointmentsService.Domain.Exceptions;
using EasyClinic.AppointmentsService.Domain.Contracts;
using EasyClinic.AppointmentsService.Infrastructure.Repositories;
using EasyClinic.AppointmentsService.Application.Helpers;
using EasyClinic.AppointmentsService.Application.Commands;
using EasyClinic.AppointmentsService.Api.Helpers;
using MassTransit;
using KEmailSender;
using EasyClinic.AppointmentsService.Application.Services;
using EasyClinic.AppointmentsService.Domain.Entities;
using Serilog;
using Microsoft.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication();

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (ctx, ex) =>
        (builder.Environment.IsDevelopment() || builder.Environment.IsStaging()) 
        && ex is not HttpResponseCodeException && ex is not ModelValidationException;
    options.ShouldLogUnhandledException = (_, ex, d) => 
        (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
        && (d.Status is null or >= 400);
    options.Map<ModelValidationException>(ex => new ProblemDetails() 
        { 
            Title = ex.Title, 
            Detail = ex.Message, 
            Status = ex.Status, 
            Type = ex.Type,
            Extensions = new Dictionary<string, object> { { "Errors", ex.Errors } }!
        });
    options.Map<HttpResponseCodeException>(ex => new ProblemDetails() 
        { 
            Title = ex.Title, 
            Detail = ex.Message, 
            Status = ex.Status, 
            Type = ex.Type 
        });
        options.MapToStatusCode<Exception>(500);
});

builder.Services.AddDbContext<AppointmentsServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AppointmentsServiceConnection"])
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

builder.Services.ConfigureAll<HttpClientFactoryOptions>(options =>
{
    options.HttpMessageHandlerBuilderActions.Add(b =>
    {
        b.AdditionalHandlers.Add(
            new PolicyHttpMessageHandler(ResiliencePolicyHelper.GetRetryPolicy()));
    });
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
    m.UsingAzureServiceBus((context, config) =>
    {
        config.Host(builder.Configuration["ConnectionStrings:AzureServiceBusConnection"]);

        config.ConfigureEndpoints(context);
    });
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IAppointmentResultsRepository, AppointmentResultsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IEmailPatternService, EmailPatternService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentCommandValidator>();

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssemblyContaining<CreateAppointmentCommand>();
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

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

await DatabaseHelper.InitializeData<AppointmentsServiceDbContext>(app);

app.Run();
