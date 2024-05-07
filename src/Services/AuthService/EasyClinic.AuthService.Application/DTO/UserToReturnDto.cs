using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Application.DTO
{
    /// <summary>
    /// Model of an ApplicationUser used to be returned to the client
    /// </summary>
    public class UserToReturnDto
    {
        public required string Id { get; set; } = default!;
        public required string Email { get; set; } = default!;
        public required string Username { get; set; } = default!;
        public required string Token { get; set; } = default!;
    }
}
