using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyClinic.OfficesService.Application.Queries
{
    public record GetOfficesQuery : IRequest
    {

    };

    public class GetOfficesQueryHandler : IRequestHandler<GetOfficesQuery>
    {
        public GetOfficesQueryHandler()
        {
        }
        public async Task Handle(GetOfficesQuery request, CancellationToken cancellationToken)
        {

        }
    }
}
