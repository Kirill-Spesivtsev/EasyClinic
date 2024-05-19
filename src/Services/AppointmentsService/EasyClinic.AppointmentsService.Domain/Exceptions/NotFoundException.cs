using System.Net;

namespace EasyClinic.AppointmentsService.Domain.Exceptions
{
    /// <summary>
    /// Exception for Not Found (404) HTTP status code.
    /// </summary>
    public class NotFoundException : HttpResponseCodeException
    {
        public NotFoundException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.NotFound;
            Title = "Not Found";
            Type = @"https://tools.ietf.org/html/rfc7231#section-6.5.4";
        }

        public NotFoundException(string entityName, Guid id) : this($"{entityName} with id: {id} was not found"){}
    }
}
