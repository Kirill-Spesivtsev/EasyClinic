using System.Net;


namespace EasyClinic.AuthService.Domain.Exceptions
{
    /// <summary>
    /// Exception for Conflict (409) HTTP status code.
    /// </summary>
    public class ConflictException : HttpResponseCodeException
    {
        public ConflictException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.Conflict;
            Title = "Conflict";
            Type = @"https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8";
        }

        public ConflictException(Guid id) : this($"User with id: {id} already exists"){}
    }
}

