using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AppointmentsService.Application.DTO
{
    public class TimeSlotDto
    {
        public TimeOnly Time { get; set; }
        public bool IsAvailable { get; set; }
    }
}
