﻿using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Application.Services
{
    public class EmailPatternService : IEmailPatternService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailPatternService(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task SendAccountConfirmEmailAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string verificationRoute = $"{_configuration["Route:PublicApiHost"]}/api/v1/auth/verify-email?token={token}";

            var message = new EmailMessageModel
            {
                Subject = "Account Email Confirmation",
                Content = $"<span style='font-family: Arial, sans-serif;'><p>Hi, {user.UserName}</p>"
                    + $"<p>Please verify your email by following the link below.</p>" 
                    + $"<p><a href='{verificationRoute}'>Verify Account</a></p><p>Thank you!</p></span>",
                From = _configuration["EmailSender:Username"]!,
                To = new List<string>{user.Email!}
            };

            await _emailSender.SendEmailAsync(message);
        }

        public async Task SendPasswordChangeEmailAsync(ApplicationUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string passwordChangeRoute = $"{_configuration["Route:PublicApiHost"]}/api/v1/account/change-password?token={token}";

            var message = new EmailMessageModel
            {
                Subject = "Account Password Change",
                Content = $"<span style='font-family: Arial, sans-serif;'><p>Hi, {user.UserName}</p>"
                    + $"<p>If you want to change your password, please follow the link below.</p>" 
                    + $"<p><a href='{passwordChangeRoute}'>Change password</a></p><p>Thank you!</p></span>",
                From = _configuration["EmailSender:Username"]!,
                To = new List<string>{user.Email!}
            };

            await _emailSender.SendEmailAsync(message);
        }
    }
}
