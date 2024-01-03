using Api.Features.Rag.Models;
using MediatR;

namespace Api.Features.Rag.Commands
{
    public record SearchCommand(SearchRequest Request) : IRequest<SearchResponse>
    {
        private class SearchCommandHandler : IRequestHandler<SearchCommand, SearchResponse>
        {
            private readonly EmbeddingModel _embeddingModel;
            private readonly VectorDb _vectorDb;
            private readonly PromptFactory _promptFactory;
            private readonly LlmProvider _llmProvider;

            public SearchCommandHandler(
                EmbeddingModel embeddingModel,
                VectorDb vectorDb,
                PromptFactory promptFactory,
                LlmProvider llmProvider)
            {
                _embeddingModel = embeddingModel;
                _vectorDb = vectorDb;
                _llmProvider = llmProvider;
                _promptFactory = promptFactory;
            }

            public async Task<SearchResponse> Handle(SearchCommand request, CancellationToken cancellationToken)
            {
                await Task.Delay(0, cancellationToken);
                // get prompt embeddings from embedding model
                var embeddings = await _embeddingModel.GetEmbeddingsForTextAsync(request.Request.Prompt, cancellationToken);
                // do embeddings similarity search from vector DB
                var searchResults = await _vectorDb.GetSimilarVectorsAsync(embeddings, cancellationToken);
                // create LLM provider prompt from template (prompt + search results)
                var prompt = _promptFactory.CreateFromSearchResults(request.Request.Prompt, searchResults);
                // get LLM provider response
                var response = await _llmProvider.GetResponseAsync(prompt, cancellationToken);
                return new SearchResponse { Response = response };
            }
        }
    }
}
