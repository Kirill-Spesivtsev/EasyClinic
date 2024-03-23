using EasyClinic.AuthService.Domain.Entities;

namespace EasyClinic.AuthService.Application.Services
{
    /// <summary>
    /// Generates JWT tokens for users
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates JWT token for user authentication
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public string GenerateJwtToken(ApplicationUser user, IList<string> roles);
    }
}
