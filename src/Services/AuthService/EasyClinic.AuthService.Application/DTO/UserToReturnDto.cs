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
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}
