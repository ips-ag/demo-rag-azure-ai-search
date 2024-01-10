namespace Api.Features.Rag.Extensions
{
    internal static class RagExtensions
    {
        public static IServiceCollection AddRag(this IServiceCollection services)
        {
            services.AddSingleton<PromptFactory>();
            return services;
        }
    }
}
