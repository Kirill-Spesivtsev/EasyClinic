using System.Net;

namespace EasyClinic.ServicesService.Domain.Exceptions
{
    /// <summary>
    /// Exception for Bad Request (400) HTTP status code.
    /// </summary>
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
