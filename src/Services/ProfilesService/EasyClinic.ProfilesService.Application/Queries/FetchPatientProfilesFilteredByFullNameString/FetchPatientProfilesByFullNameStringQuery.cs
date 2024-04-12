using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.ProfilesService.Application.Queries.GetPatientProfilesByName;

/// <summary>
/// Query to Get Patient Profiles by name string template.
/// </summary>
public record FetchPatientProfilesByFullNameStringQuery : IRequest<List<PatientProfile>> 
{
    public required string FullNameString { get; init; }
};

/// <summary>
/// Handler for <see cref="FetchPatientProfilesByFullNameStringQuery"/>
/// </summary>
public class FetchPatientProfilesByFullNameStringQueryHandler : IRequestHandler<FetchPatientProfilesByFullNameStringQuery, List<PatientProfile>>
{
    private readonly IRepository<PatientProfile> _profilesRepository;

    public FetchPatientProfilesByFullNameStringQueryHandler(IRepository<PatientProfile> profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves Patient Profiles by name string template.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<PatientProfile>> Handle(FetchPatientProfilesByFullNameStringQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository
            .GetFilteredAsync(p => EF.Functions.Like(p.FullName, $"{request.FullNameString}%"));
    }
}
