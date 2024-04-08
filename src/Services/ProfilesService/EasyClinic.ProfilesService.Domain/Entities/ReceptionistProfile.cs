using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Entities
{
    public class ReceptionistProfile : ProfileBase
    {
        public string Email { get; set; } = default!;

        public string OfficeId { get; set; } = default!;
    }
}
