namespace EasyClinic.AppointmentsService.Domain.Exceptions
{
    /// <summary>
    /// Base class for all HTTP response code exceptions.
    /// </summary>
    public abstract class HttpResponseCodeException : Exception 
    { 
        protected HttpResponseCodeException(string message) : base (message){ }

        public int Status { get; init; }

        public string Title { get; init; } = default!;

        public string Type { get; init; } = default!;
    }
}
