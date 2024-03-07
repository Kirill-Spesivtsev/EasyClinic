namespace EasyClinic.OfficesService.Domain.Exceptions
{
    public abstract class HttpResponseCodeException : Exception 
    { 
        protected HttpResponseCodeException(string message) : base (message){ }

        public int Status { get; init; }

        public string Title { get; init; } = default!;

        public string Type { get; init; } = default!;
    }
}
