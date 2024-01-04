using Api.Azure.OpenAi.Configuration;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;

namespace Api.Features.Rag
{
    internal class LlmProvider
    {
        private readonly IOptionsMonitor<OpenAiOptions> _configuration;
        private readonly OpenAIClient _client;

        public LlmProvider(IOptionsMonitor<OpenAiOptions> configuration, OpenAIClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task<string> GetResponseAsync(string prompt, CancellationToken cancellationToken)
        {
            var configuration = _configuration.CurrentValue;
            var completion = configuration.Completion;
            var options = new CompletionsOptions
            {
                DeploymentName = completion.DeploymentName,
                Temperature = completion.Temperature,
                MaxTokens = completion.MaxTokens,
                NucleusSamplingFactor = completion.TopP,
                FrequencyPenalty = completion.FrequencyPenalty,
                PresencePenalty = completion.PresencePenalty,
                Prompts = { prompt }
            };
            var response = await _client.GetCompletionsAsync(options, cancellationToken);
            var choices = response.Value.Choices;
            return choices.Count > 0 ? choices[0].Text : string.Empty;
        }
    }
}
