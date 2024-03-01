namespace EasyClinic.AuthService.Domain.Exceptions
{
    public abstract class HttpResponseCodeException : Exception 
    { 
        protected HttpResponseCodeException(string message) : base (message){ }

        public int Status { get; init; }

        public string Title { get; init; }

        public string Type { get; init; }
    }
}
