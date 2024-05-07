using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Application.Queries.GetAllUserRolesById;

public class GetAllUserRolesByIdQuery : IRequest<IEnumerable<string>>
{
    public string UserId { get; set; }
}

public class GetAllUserRolesByIdQueryHandler : IRequestHandler<GetAllUserRolesByIdQuery, IEnumerable<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetAllUserRolesByIdQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<string>> Handle(GetAllUserRolesByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user == null)
        {
            throw new NotFoundException($"User with id {request.UserId} not found");
        }

        var roles = await _userManager.GetRolesAsync(user);

        return roles;
    }
}
