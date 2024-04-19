using EasyClinic.ServicesService.Domain.Enums;

namespace EasyClinic.ServicesService.Domain.Entities
{
    /// <summary>
    /// Service Medical Specialization
    /// </summary>
    public class Specialization
    {
        public Guid Id { get;set; }

        public string Name { get;set; } = null!;

        public Status Status { get; set; }
    }
}