using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Domain.Enums
{
    /// <summary>
    /// Represents the status of an <see cref="Domain.Entities.Office"/> (active or inactive).
    /// </summary>
    public enum OfficeStatus: byte
    {
        Available = 0,
        Occupied = 1
    }
}
