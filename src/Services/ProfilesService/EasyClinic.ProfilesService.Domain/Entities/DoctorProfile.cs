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
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = default!;

        public MedicalSpecialization Specialization { get; set; }  = default!;

        public string OfficeId { get; set; } = default!;

        public string CareerStartYear { get; set; } = default!;

        public EmployeeStatus Status { get; set; } = default!;
    }
}
