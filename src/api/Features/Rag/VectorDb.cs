using Api.Azure.Search;
using Api.Features.Rag.Models;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Api.Features.Rag
{
    internal class VectorDb
    {
        private const string IndexName = "hotels";
        private readonly SearchClientFactory _searchClientFactory;

        public VectorDb(SearchClientFactory searchClientFactory)
        {
            _searchClientFactory = searchClientFactory;
        }

        public async Task<IReadOnlyCollection<EntityResponse>> GetSimilarVectorsAsync(
            ReadOnlyMemory<float> vectors,
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
                var entity = new EntityResponse(doc.Id, doc.Name, doc.Description, result.Score);
                searchResults.Add(entity);
            }
            return searchResults;
        }

        private class Entity
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public ReadOnlyMemory<float> DescriptionVector { get; set; }
            public string Category { get; set; }
            public ReadOnlyMemory<float> CategoryVector { get; set; }
        }
    }
}
