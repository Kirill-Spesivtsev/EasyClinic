using EasyClinic.OfficesService.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.DTO
{
    public class OfficeDto
    {
        public int Id { get; set; }
        public IFormFile PhotoBlob { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string RegistryPhone { get; set; }

        public OfficeStatus Status { get; set; }

        public string Address 
        {   
            get
            {
                return $"{City}, {Street}, {HouseNumber}, office {OfficeNumber}";
            }
        }
    }
}
