using Api.Features.Core;
using Api.Features.Core.Domain;
using Api.Features.Core.VectorDb;
using Api.Features.Core.VectorDb.Models;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Api.Azure.Search
{
    internal class AzureAiSearch : IVectorDb, IKeywordSearch
    {
        private const string IndexName = "books";
        private readonly SearchClientFactory _searchClientFactory;

        public AzureAiSearch(SearchClientFactory searchClientFactory)
        {
            _searchClientFactory = searchClientFactory;
        }

        public async Task<IReadOnlyCollection<EntityResponse>> GetByKeywordAsync(
            string query,
            CancellationToken cancellationToken)
        {
            var searchClient = _searchClientFactory.CreateForIndex(IndexName);
            var searchOptions = new SearchOptions
            {
                QueryType = SearchQueryType.Simple, IncludeTotalCount = false, SearchMode = SearchMode.Any
            };
            SearchResults<Entity> response = await searchClient.SearchAsync<Entity>(
                searchText: query,
                options: searchOptions,
                cancellationToken: cancellationToken);
            var searchResults = new List<EntityResponse>();
            await foreach (var result in response.GetResultsAsync())
            {
                var doc = result.Document;
                var entity = new EntityResponse(doc.Id, doc.Name, doc.Description, doc.Author, doc.Year);
                searchResults.Add(entity);
            }
            return searchResults;
        }

        public async Task<IReadOnlyCollection<EntityResponse>> GetByVectorSimilarityAsync(
            float[] vectors,
            CancellationToken cancellationToken)
        {
            var searchClient = _searchClientFactory.CreateForIndex(IndexName);
            var searchOptions = new SearchOptions
            {
                VectorSearch = new VectorSearchOptions
                {
                    Queries =
                    {
                        new VectorizedQuery(vectors)
                        {
                            KNearestNeighborsCount = 3, Fields = { nameof(Entity.DescriptionVector) }
                        }
                    }
                }
            };
            SearchResults<Entity> response = await searchClient.SearchAsync<Entity>(
                searchText: null,
                options: searchOptions,
                cancellationToken: cancellationToken);
            var searchResults = new List<EntityResponse>();
            await foreach (var result in response.GetResultsAsync())
            {
                var doc = result.Document;
                var entity = new EntityResponse(doc.Id, doc.Name, doc.Description, doc.Author, doc.Year);
                searchResults.Add(entity);
            }
            return searchResults;
        }
    }
}
