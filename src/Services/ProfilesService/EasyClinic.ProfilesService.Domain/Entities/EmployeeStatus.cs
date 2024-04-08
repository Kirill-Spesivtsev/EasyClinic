using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Entities;
public class EmployeeStatus
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}
