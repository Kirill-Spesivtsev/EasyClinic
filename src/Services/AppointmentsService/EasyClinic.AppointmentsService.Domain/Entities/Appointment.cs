using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AppointmentsService.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DoctorId { get; set; }
        public string DoctorFullName { get; set; } = null!;

        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;

        public Guid PatientId { get; set; }
        public string PatientFullName { get; set; } = null!;
        public string PatientPhone { get; set; } = null!;
        public string? PatientEmail { get; set; } = null!;
        public string? PatientUserName { get; set; } = null!;

        public Guid OfficeId { get; set; }
        public string OfficeName { get; set; } = null!;

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool IsApproved { get; set; } = false;
        
    }
}
