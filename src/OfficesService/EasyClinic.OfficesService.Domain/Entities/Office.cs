﻿using EasyClinic.OfficesService.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EasyClinic.OfficesService.Domain.Entities
{
    public class Office
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
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
