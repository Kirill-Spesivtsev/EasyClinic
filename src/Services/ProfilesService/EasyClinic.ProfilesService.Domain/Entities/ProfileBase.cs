using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyClinic.ProfilesService.Domain.Entities
{
    public abstract class ProfileBase
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; } = default!;

        public string? PhotoPath { get; set; } = default!;

        public string? AccountId { get; set; } = default!;

        [NotMapped]
        public bool IsLinkedToAccount 
        { 
            get => !string.IsNullOrEmpty(AccountId); 
        }

    }
}
