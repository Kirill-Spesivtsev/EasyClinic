using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AppointmentsService.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Url { get; set; } = null!;

        public Guid AppointmentResultId { get; set; }
        public virtual AppointmentResult AppointmentResult { get; set; } = null!;
        
    }
}
