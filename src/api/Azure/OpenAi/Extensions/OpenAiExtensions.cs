using Api.Azure.OpenAi.Configuration;

namespace Api.Azure.OpenAi.Extensions
{
    public static class OpenAiExtensions
    {
        public static IServiceCollection AddOpenAi(this IServiceCollection services)
        {
            services.AddOptions<OpenAiOptions>()
                .BindConfiguration(OpenAiOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();
            return services;
        }
    }
}
