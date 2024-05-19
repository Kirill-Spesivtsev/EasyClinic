using EasyClinic.AppointmentsService.Domain.Entities;
using KEmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EasyClinic.AppointmentsService.Application.Services
{
    /// <summary>
    /// Class to consctruct patterns for email messages.
    /// Implementation of <see cref="IEmailPatternService">. 
    /// </summary>
    public class EmailPatternService : IEmailPatternService
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        public EmailPatternService(IConfiguration configuration,
            IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSender = emailSender;
        }

        /// <summary>
        /// Creates email pattern to send appointment results to user email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendAppointmentResultsEmail(AppointmentResult appointmentResult, DateOnly date, TimeOnly time, string email, string username)
        {
            var message = new EmailMessageModel
            {
                Subject = $"Here are your appointment results for {date}, {time}",
                Content = $"<span style='font-family: Arial, sans-serif;'><p>Hi, {username}</p>"
                    + $"<p>Thank you fou using our medical services!</p></span>",
                From = _configuration["EmailSender:Username"]!,
                To = new List<string>{email}
            };

            await _emailSender.SendEmailAsync(message);
        }

        /// <summary>
        /// Creates email pattern to send appointment results to user email.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task SendAppointmentReminderEmail(DateOnly date, TimeOnly time, string email, string username)
        {
            var message = new EmailMessageModel
            {
                Subject = "Tomorrow doctor appointment reminder",
                Content = $"<span style='font-family: Arial, sans-serif;'><p>Hi, {username}</p>"
                    + $"<p>You have an appointment tomorrow({date}) at {time}.</p>"
                    + $"<p>Thank you fou using our medical services!</p></span>",
                From = _configuration["EmailSender:Username"]!,
                To = new List<string>{email}
            };

            await _emailSender.SendEmailAsync(message);
        }

    }
}
