using Api.Azure.OpenAi.Configuration;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace Api.Features.Rag
{
    public class EmbeddingModel
    {
        private readonly IOptionsMonitor<OpenAiOptions> _configuration;
        private readonly OpenAIClient _client;

        public EmbeddingModel(IOptionsMonitor<OpenAiOptions> configuration, OpenAIClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task<ReadOnlyMemory<float>> GetEmbeddingsForTextAsync(string text, CancellationToken cancellationToken)
        {
            var configuration = _configuration.CurrentValue;
            var adjustedText = text.ReplaceLineEndings(" ");
            var options = new EmbeddingsOptions
            {
                DeploymentName = configuration.Embedding.DeploymentName, Input = { adjustedText }
            };
            Embeddings embeddings = await _client.GetEmbeddingsAsync(options, cancellationToken);
            return embeddings.Data[0].Embedding;
        }

        public async Task<int> GetEmbeddingsDimensionsAsync(CancellationToken cancellationToken)
        {
            var configuration = _configuration.CurrentValue;
            var options = new EmbeddingsOptions
            {
                DeploymentName = configuration.Embedding.DeploymentName, Input = { "test" }
            };
            Embeddings embeddings = await _client.GetEmbeddingsAsync(options, cancellationToken);
            return embeddings.Data[0].Embedding.Length;
        }
    }
}
