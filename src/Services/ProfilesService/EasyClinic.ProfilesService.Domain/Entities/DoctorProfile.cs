using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Entities
{
    public class DoctorProfile : ProfileBase
    {
        public DateOnly DateOfBirth { get; set; }

        public string Email { get; set; } = default!;

        public Guid OfficeId { get; set; }

        public string CareerStartYear { get; set; } = default!;

        public Guid MedicalSpecializationId { get; set; }
        public MedicalSpecialization MedicalSpecialization { get; set; }  = default!;

        public Guid EmployeeStatusId { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; } = default!;
    }
}
