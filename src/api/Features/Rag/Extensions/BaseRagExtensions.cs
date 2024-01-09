namespace Api.Features.Rag.Extensions
{
    public static class BaseRagExtensions
    {
        public static IServiceCollection AddBaseRag(this IServiceCollection services)
        {
            services.AddScoped<EmbeddingModel>();
            services.AddScoped<VectorDb>();
            services.AddScoped<LlmProvider>();
            services.AddSingleton<PromptFactory>();
            return services;
        }
    }
}
