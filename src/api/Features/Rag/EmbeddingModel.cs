using Api.Azure.OpenAi.Configuration;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace Api.Features.Rag
{
    internal class EmbeddingModel
    {
        private readonly IOptionsMonitor<OpenAiOptions> _configuration;
        private readonly OpenAIClient _client;

        public EmbeddingModel(IOptionsMonitor<OpenAiOptions> configuration, OpenAIClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task<IReadOnlyList<float>> GetEmbeddingsForTextAsync(string text, CancellationToken cancellationToken)
        {
            var configuration = _configuration.CurrentValue;
            var adjustedText = text.ReplaceLineEndings(" ");
            var options = new EmbeddingsOptions
            {
                DeploymentName = configuration.Embedding.DeploymentName, Input = { adjustedText }
            };
            var response = await _client.GetEmbeddingsAsync(options, cancellationToken);
            return response.Value.Data[0].Embedding.ToArray();
        }
    }
}
