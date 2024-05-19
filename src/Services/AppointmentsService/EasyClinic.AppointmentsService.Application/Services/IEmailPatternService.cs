using EasyClinic.AppointmentsService.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace EasyClinic.AppointmentsService.Application.Services
{
    /// <summary>
    /// Interface to consctuct patterns for email messages.
    /// </summary>
    public interface IEmailPatternService
    {
        /// <summary>
        /// Constructs email pattern for appointment results.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendAppointmentResultsEmail(AppointmentResult appointmentResult, DateOnly date, TimeOnly time, string email, string username);

        /// <summary>
        /// Constructs email pattern for appointment reminder.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task SendAppointmentReminderEmail(DateOnly date, TimeOnly time, string email, string username);
    }
}
