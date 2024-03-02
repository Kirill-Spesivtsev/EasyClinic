using MediatR;


namespace EasyClinic.AuthService.Application.Queries
{
    public record CheckEmailExistenceQuery : IRequest<bool> 
    {
        public string Email { get;set; } = default!;
    }

}
