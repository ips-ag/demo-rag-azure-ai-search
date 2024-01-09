using Api.Azure.Search;
using Api.Features.Rag.Models;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Api.Features.Rag
{
    internal class VectorDb
    {
        private const string IndexName = "books";
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
                var entity = new EntityResponse(doc.Id, doc.Name, doc.Description, doc.Author, doc.Year, result.Score);
                searchResults.Add(entity);
            }
            return searchResults;
        }

        // ReSharper disable UnusedAutoPropertyAccessor.Local
        private class Entity
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public int Year { get; set; }
            public string Description { get; set; }
            public ReadOnlyMemory<float> DescriptionVector { get; set; }
        }
        // ReSharper restore UnusedAutoPropertyAccessor.Local
    }
}
