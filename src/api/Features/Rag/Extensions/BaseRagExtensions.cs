namespace Api.Features.Rag.Extensions
{
    internal static class BaseRagExtensions
    {
        public static IServiceCollection AddBaseRag(this IServiceCollection services)
        {
            services.AddSingleton<PromptFactory>();
            return services;
        }
    }
}
