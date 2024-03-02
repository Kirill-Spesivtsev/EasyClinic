using EasyClinic.AuthService.Domain.Entities;
using EasyClinic.AuthService.Domain.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.AuthService.Application.Queries
{
    internal class CheckEmailExistenceQueryHandler : IRequestHandler<CheckEmailExistenceQuery, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CheckEmailExistenceQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Handle(CheckEmailExistenceQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(request.Email);
            return result != null;
        }
    }
}
