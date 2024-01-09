using Api.Azure.OpenAi.Configuration;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace Api.Azure.OpenAi.Extensions
{
    public static class OpenAiExtensions
    {
        public static IServiceCollection AddOpenAi(this IServiceCollection services)
        {
            services.AddOptions<OpenAiOptions>()
                .BindConfiguration(OpenAiOptions.SectionName)
                .ValidateDataAnnotations();
            services.AddScoped<OpenAIClient>(
                sp =>
                {
                    var options = sp.GetRequiredService<IOptions<OpenAiOptions>>();
                    var configuration = options.Value;
                    var endpoint = new Uri(configuration.Endpoint);
                    var keyCredential = new AzureKeyCredential(configuration.ApiKey);
                    return new OpenAIClient(endpoint, keyCredential);
                });
            return services;
        }
    }
}
