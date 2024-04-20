using EasyClinic.ServicesService.Domain.Entities;
using EasyClinic.ServicesService.Domain.Enums;

namespace EasyClinic.ServicesService.Application.DTO
{
    public class SpecializationDto
    {
        public string Name { get; set; } = null!;
        public Status Status { get; set; }
    }
}
