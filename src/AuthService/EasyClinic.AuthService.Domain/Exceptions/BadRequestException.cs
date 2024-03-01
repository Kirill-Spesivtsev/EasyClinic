using System.Net;

namespace EasyClinic.AuthService.Domain.Exceptions
{
    public class BadRequestException : HttpResponseCodeException
    {
        public BadRequestException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.BadRequest;
            Title = "Bad Request";
            Type = @"https://tools.ietf.org/html/rfc7231#section-6.5.1";
        }

    }
}
