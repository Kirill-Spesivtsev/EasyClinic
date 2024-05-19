using AutoMapper;
using EasyClinic.AppointmentsService.Application.Commands;
using EasyClinic.AppointmentsService.Domain.Entities;

namespace EasyClinic.AppointmentsService.Application.Helpers
{
    /// <summary>
    /// AutoMapper mapping profiles configuration
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateAppointmentCommand, Appointment>();

            CreateMap<CreateAppointmentResultCommand, AppointmentResult>();

            CreateMap<UpdateAppointmentResultCommand, AppointmentResult>();
        }
    }
}