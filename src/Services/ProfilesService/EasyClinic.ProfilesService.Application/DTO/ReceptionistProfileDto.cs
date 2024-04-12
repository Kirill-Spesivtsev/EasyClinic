using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.DTO
{
    public class ReceptionistProfileDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; } = default!;

        public string? PhotoPath { get; set; } = default!;

        public string? AccountId { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string OfficeId { get; set; } = default!;
    }
}
