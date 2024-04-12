using EasyClinic.ProfilesService.Domain.Contracts;
using EasyClinic.ProfilesService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyClinic.OfficesService.Application.Queries;

/// <summary>
/// Query to Fetch Doctor Profiles by name string template.
/// </summary>
public record FetchDoctorProfilesByFullNameStringQuery : IRequest<List<DoctorProfile>> 
{
    public required string FullNameString { get; init; }
};

/// <summary>
/// Handler for <see cref="FetchDoctorProfilesByFullNameStringQuery"/>
/// </summary>
public class FetchDoctorProfilesByFullNameStringQueryHandler : IRequestHandler<FetchDoctorProfilesByFullNameStringQuery, List<DoctorProfile>>
{
    private readonly IDoctorProfilesRepository _profilesRepository;
    public FetchDoctorProfilesByFullNameStringQueryHandler(IDoctorProfilesRepository profilesRepository)
    {
        _profilesRepository = profilesRepository;
    }

    /// <summary>
    /// Retrieves Doctor Profiles by name string template.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<DoctorProfile>> Handle(FetchDoctorProfilesByFullNameStringQuery request, CancellationToken cancellationToken)
    {
        return await _profilesRepository
            .GetFilteredAsync(p => EF.Functions.Like(p.FullName, $"{request.FullNameString}%"));
    }
}

