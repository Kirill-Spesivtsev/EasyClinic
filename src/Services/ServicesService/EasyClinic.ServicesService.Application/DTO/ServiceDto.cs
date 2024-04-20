
using EasyClinic.ServicesService.Domain.Enums;

namespace EasyClinic.ServicesService.Application.DTO
{
    public class ServiceDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public Guid CategoryId { get; set; }  

        public Guid SpecializationId { get; set; }
    }
}
