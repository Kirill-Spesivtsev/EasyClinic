using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Services
{
    public interface IEmailPatternService
    {
        public Task SendAccountConfirmEmailAsync(ApplicationUser user);

        public Task SendPasswordChangeEmailAsync(ApplicationUser user);
    }
}
