using EasyClinic.OfficesService.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyClinic.OfficesService.Domain.Entities
{
    /// <summary>
    /// Represents an Offices table in database.
    /// </summary>
    public class Office
    {
        /// <summary>
        /// Identifier, PK
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Image of the Office
        /// </summary>
        public string? PhotoPath { get; set; }

        /// <summary>
        /// Address: City
        /// </summary>
        public string City { get; set; } = default!;

        /// <summary>
        /// Address: Street
        /// </summary>
        public string Street { get; set; } = default!;

        /// <summary>
        /// Address: HouseNumber
        /// </summary>
        public int HouseNumber { get; set; } = default!;

        /// <summary>
        /// Address: HouseNumber
        /// </summary>
        public int? OfficeNumber { get; set; } = default!;

        /// <summary>
        /// Registry Phone
        /// </summary>
        public string RegistryPhone { get; set; } = default!;

        /// <summary>
        /// Office Status (Active or Inactive)
        /// </summary>
        public OfficeStatus Status { get; set; }

        /// <summary>
        /// Full Address of the Office
        /// </summary>
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
