using Api.Azure.OpenAi.Configuration;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace Api.Features.Rag
{
    internal class EmbeddingModel
    {
        private readonly IOptionsMonitor<OpenAiOptions> _configuration;

        public EmbeddingModel(IOptionsMonitor<OpenAiOptions> configuration)
        {
            _configuration = configuration;
        }

        public async Task<float[]> GetEmbeddingsForTextAsync(string text, CancellationToken cancellationToken)
        {
            var configuration = _configuration.CurrentValue;
            var client = CreateOpenAiClient(configuration);
            var adjustedText = text.ReplaceLineEndings(" ");
            var options = new EmbeddingsOptions
            {
                DeploymentName = configuration.Embedding.DeploymentName, Input = { adjustedText }
            };
            var response = await client.GetEmbeddingsAsync(options, cancellationToken);
            return response.Value.Data[0].Embedding.ToArray();
        }

        private OpenAIClient CreateOpenAiClient(OpenAiOptions configuration)
        {
            var oaiEndpoint = new Uri(configuration.Endpoint);
            var key = configuration.ApiKey;
            var credentials = new AzureKeyCredential(key);
            return new OpenAIClient(oaiEndpoint, credentials);
        }
    }
}
