using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.DTO
{
    public class PatientProfileDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; } = default!;

        public string? PhotoPath { get; set; } = default!;

        public string? AccountId { get; set; } = default!;

        public DateOnly DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = default!;
    }
}
