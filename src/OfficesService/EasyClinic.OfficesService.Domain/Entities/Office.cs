using EasyClinic.OfficesService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyClinic.OfficesService.Domain.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public string? PhotoPath { get; set; }
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int HouseNumber { get; set; } = default!;
        public int? OfficeNumber { get; set; } = default!;
        public string RegistryPhone { get; set; } = default!;

        public OfficeStatus Status { get; set; }

        [NotMapped]
        public string Address 
        {   
            get
            {
                return $"{City}, {Street}, {HouseNumber}"
                + (OfficeNumber.HasValue ? $", office {OfficeNumber}" : string.Empty);
            }
        }

        
    }
}
