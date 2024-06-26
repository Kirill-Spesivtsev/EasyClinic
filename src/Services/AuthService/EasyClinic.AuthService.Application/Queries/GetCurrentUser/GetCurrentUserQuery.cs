﻿using EasyClinic.AuthService.Application.DTO;
using EasyClinic.AuthService.Application.Services;
using EasyClinic.AuthService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EasyClinic.AuthService.Application.Queries.GetCurrentUser
{
    /// <summary>
    /// Query to get the current authenticeted user
    /// </summary>
    public record GetCurrentUserQuery : IRequest<UserToReturnDto>
    {
        public ClaimsPrincipal User { get; set; } = default!;
    };

    /// <summary>
    /// Handler for <see cref="GetCurrentUserQuery"/>
    /// </summary>
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserToReturnDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public GetCurrentUserQueryHandler(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Handles the <see cref="GetCurrentUserQuery"/>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Current authenticated ApplicationUser data</returns>
        public async Task<UserToReturnDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(request.User);
            var userRoles = await _userManager.GetRolesAsync(user!);

            var result = new UserToReturnDto
            {
                Id = user?.Id!,
                Email = user?.Email!,
                Username = user?.UserName!,
                Token = _tokenService.GenerateJwtToken(user!, userRoles)
            };

            return result;
        }
    }
}
