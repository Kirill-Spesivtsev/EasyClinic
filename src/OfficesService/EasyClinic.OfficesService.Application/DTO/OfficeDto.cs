using EasyClinic.OfficesService.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.DTO
{
    public class OfficeDto
    {
        public string? PhotoPath { get; set; }
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int HouseNumber { get; set; } = default!;
        public int? OfficeNumber { get; set; } = default!;
        public string RegistryPhone { get; set; } = default!;

        public OfficeStatus Status { get; set; }

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
