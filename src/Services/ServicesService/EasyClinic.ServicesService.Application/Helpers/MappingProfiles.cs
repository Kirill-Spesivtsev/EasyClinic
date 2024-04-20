using AutoMapper;
using EasyClinic.ServicesService.Application.DTO;
using EasyClinic.ServicesService.Domain.Entities;

namespace EasyClinic.ServicesService.Application.Helpers
{
    /// <summary>
    /// AutoMapper mapping profiles configuration
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ServiceDto, Service>();

            CreateMap<SpecializationDto, Specialization>();
        }
    }
}