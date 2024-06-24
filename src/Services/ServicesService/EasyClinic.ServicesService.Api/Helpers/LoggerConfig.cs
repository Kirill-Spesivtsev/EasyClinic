using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Serilog.Exceptions;

namespace EasyClinic.ServicesService.Api.Helpers
{
    /// <summary>
    /// Logger configuration class
    /// </summary>
    public static class LoggerConfig
    {
        /// <summary>
        /// Logger configuration
        /// </summary>
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
        (context, config) =>
        {
            var env = context.HostingEnvironment;
            config.MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);
                
            if (context.HostingEnvironment.IsDevelopment() || context.HostingEnvironment.IsStaging())
            {
                config.MinimumLevel.Override("EasyClinic.Services", LogEventLevel.Debug);
                config.WriteTo.Console();
            }

            var elasticUrl = context.Configuration["ElasticSearch:Uri"];
            if (!string.IsNullOrEmpty(elasticUrl))
            {
                config.WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
                        IndexFormat = $"{context.Configuration["ApplicationName"]?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyyMM}",
                        MinimumLogEventLevel = LogEventLevel.Debug,
                        ModifyConnectionSettings = x => x.BasicAuthentication(
                            context.Configuration["ElasticSearch:User"],
                            context.Configuration["ElasticSearch:Password"]),
                    });
            }
        };
    }
}
