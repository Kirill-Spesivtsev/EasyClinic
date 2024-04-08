using AutoMapper;
using EasyClinic.ProfilesService.Application.Commands;
using EasyClinic.ProfilesService.Application.DTO;
using EasyClinic.ProfilesService.Domain.Entities;

namespace EasyClinic.ProfilesService.Application.Helpers
{
    /// <summary>
    /// AutoMapper profiles configuration
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PatientProfileDto, PatientProfile>();
        }
    }
}