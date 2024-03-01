using EasyClinic.AuthService.Domain.Entities;

namespace EasyClinic.AuthService.Application.Services
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user);
    }
}
