using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AppointmentsService.Domain.Entities
{
    public class AppointmentResult
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Complaints { get; set; } = null!;

        public string Conclusion { get; set; } = null!;

        public string Recommendations { get; set; } = null!;

        public Guid AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; } = null!;

        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        
    }
}
