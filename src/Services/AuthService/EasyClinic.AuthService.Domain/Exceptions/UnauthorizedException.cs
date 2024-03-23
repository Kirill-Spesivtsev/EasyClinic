using System.Net;

namespace EasyClinic.AuthService.Domain.Exceptions
{
    /// <summary>
    /// Exception for Unauthorized (401) HTTP status code.
    /// </summary>
    public class UnauthorizedException : HttpResponseCodeException
    {
        public UnauthorizedException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.Unauthorized;
            Title = "Unauthorized";
            Type = @"https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
        }

        public UnauthorizedException() 
            : this($"This action requires authentication. Please login to your account first"){}
    }
}

