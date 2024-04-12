using EasyClinic.ProfilesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.DTO
{
    public class DoctorProfileDto
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; } = default!;

        public string? PhotoPath { get; set; } = default!;

        public string? AccountId { get; set; } = default!;

        public DateOnly DateOfBirth { get; set; }

        public string Email { get; set; } = default!;

        public Guid OfficeId { get; set; }

        public string CareerStartYear { get; set; } = default!;

        public Guid MedicalSpecializationId { get; set; }

        public Guid EmployeeStatusId { get; set; }    
    }
}
