using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AuthService.Application.Services
{
    /// <summary>
    /// Used to consctuct patterns for email messages.
    /// </summary>
    public interface IEmailPatternService
    {
        /// <summary>
        /// Constructs email pattern for confirming account.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendAccountConfirmEmailAsync(ApplicationUser user);

        /// <summary>
        /// Constructs email pattern for changing password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendPasswordChangeEmailAsync(ApplicationUser user);
    }
}
