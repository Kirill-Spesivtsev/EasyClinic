using AutoMapper;
using EasyClinic.OfficesService.Application.DTO;
using EasyClinic.OfficesService.Domain.Entities;

namespace EasyClinic.OfficesService.Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<OfficeDto, Office>()
                .ForMember(o => o.Id, opt => opt.Ignore());

            CreateMap<Office, Office>()
                .ForMember(o => o.Id, opt => opt.Ignore());
        }
    }
}