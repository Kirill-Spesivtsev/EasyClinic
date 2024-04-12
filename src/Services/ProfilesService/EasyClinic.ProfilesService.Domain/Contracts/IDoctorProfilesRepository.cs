using EasyClinic.ProfilesService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Domain.Contracts
{
    /// <summary>
    /// Repository interface for DoctorProfiles.
    /// </summary>
    public interface IDoctorProfilesRepository : IRepository<DoctorProfile>
    {

    }
}
