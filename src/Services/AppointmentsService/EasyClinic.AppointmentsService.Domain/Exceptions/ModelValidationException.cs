using System.Net;

namespace EasyClinic.AppointmentsService.Domain.Exceptions
{
    /// <summary>
    /// Exception for model validation fail.
    /// </summary>
    public class ModelValidationException : Exception
    {
        public int Status { get; init; }

        public string Title { get; init; }

        public string Type { get; init; }

        public object Errors { get; set;} = null!;

        public ModelValidationException(string message = "One or more validation errors occurred.") : base(message)
        {
            Status = (int)HttpStatusCode.BadRequest;
            Title = "Bad Request";
            Type = @"https://tools.ietf.org/html/rfc7231#section-6.5.1";
        }

    }
}
