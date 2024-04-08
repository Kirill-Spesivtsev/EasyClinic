using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Entities
{
    public class PatientProfile : ProfileBase
    {
        public DateOnly DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = default!;
    }
}
