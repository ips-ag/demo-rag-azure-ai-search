namespace Api.Features.HybridRag.Extensions
{
    internal static class HybridRagExtensions
    {
        public static IServiceCollection AddHybridRag(this IServiceCollection services)
        {
            services.AddSingleton<PromptFactory>();
            return services;
        }
    }
}
