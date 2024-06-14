using Polly.Extensions.Http;
using Polly;

namespace EasyClinic.AppointmentsService.Api.Helpers
{
    public static class ResiliencePolicyHelper
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()  
        { 
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
