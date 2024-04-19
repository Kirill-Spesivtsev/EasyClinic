using EasyClinic.ServicesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Application.DTO
{
    public class ServiceDto
    {
        public Guid Id { get;set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Status { get; set; } = null!;

        public Guid CategoryId { get; set; }  

        public Guid SpecializationId { get; set; }
    }
}
