using EasyClinic.ServicesService.Domain.Contracts;
using EasyClinic.ServicesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ServicesService.Domain.Contracts
{
    /// <summary>
    /// Repository interface for Profiles.
    /// </summary>
    public interface IServicesRepository : IRepository<Service>
    {

    }
}
