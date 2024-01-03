using Azure;
using Azure.AI.OpenAI;

namespace Api.Features.Rag
{
    internal class EmbeddingModel
    {
        public async Task<float[]> GetEmbeddingsForPromptAsync(string prompt, CancellationToken cancellationToken)
        {
            var oaiEndpoint = new Uri("https://oaicptlpoc.openai.azure.com");
            var key = "YOUR_API_KEY";
            var credentials = new AzureKeyCredential(key);
            var client = new OpenAIClient(oaiEndpoint, credentials);
            var options = new EmbeddingsOptions
            {
                DeploymentName = "text-embedding-ada-002", Input = { prompt.ReplaceLineEndings(" ") }
            };
            var response = await client.GetEmbeddingsAsync(options, cancellationToken);
            return response.Value.Data[0].Embedding.ToArray();
        }
    }
}
