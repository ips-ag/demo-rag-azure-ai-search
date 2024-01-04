using Api.Azure.Search;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;

namespace Api.Features.Rag
{
    internal class VectorDb
    {
        private const string IndexName = "hotels";
        private readonly SearchClientFactory _searchClientFactory;
        private readonly SearchIndexClient _searchIndexClient;
        private readonly EmbeddingModel _embeddingModel;

        public VectorDb(
            SearchClientFactory searchClientFactory,
            EmbeddingModel embeddingModel,
            SearchIndexClient searchIndexClient)
        {
            _searchClientFactory = searchClientFactory;
            _embeddingModel = embeddingModel;
            _searchIndexClient = searchIndexClient;
        }

        public async Task<string[]> GetSimilarVectorsAsync(
            IReadOnlyList<float> vectors,
            CancellationToken cancellationToken)
        {
            // await PopulateIndexAsync(cancellationToken);

            // var searchClient = _searchClientFactory.CreateForIndex(IndexName);
            // SearchResults<Hotel> response = await searchClient.SearchAsync<Hotel>(null,
            //     new SearchOptions(vectorQueries:{ new VectorizedQuery() { Vector = vectorizedResult, KNearestNeighborsCount = 3, Fields = { "DescriptionVector" } } },
            //     cancellationToken);
            //
            // int count = 0;
            // Console.WriteLine($"Single Vector Search Results:");
            // await foreach (SearchResult<Hotel> result in response.GetResultsAsync())
            // {
            //     count++;
            //     Hotel doc = result.Document;
            //     Console.WriteLine($"{doc.HotelId}: {doc.HotelName}");
            // }
            // Console.WriteLine($"Total number of search results:{count}");
            throw new NotImplementedException();
        }

        private async Task PopulateIndexAsync(CancellationToken cancellationToken)
        {
            // create index
            var vectorSearchProfile = "my-vector-profile";
            var vectorSearchHnswConfig = "my-hsnw-vector-config";
            var vectorSearchKnnConfig = "my-knn-vector-config";
            var modelDimensions = 1536;
            SearchIndex searchIndex = new(IndexName)
            {
                Fields =
                {
                    new SimpleField("HotelId", SearchFieldDataType.String)
                    {
                        IsKey = true, IsFilterable = true, IsSortable = true, IsFacetable = true
                    },
                    new SearchableField("HotelName") { IsFilterable = true, IsSortable = true },
                    new SearchableField("Description") { IsFilterable = true },
                    new SearchField(
                        "DescriptionVector",
                        SearchFieldDataType.Collection(SearchFieldDataType.Single))
                    {
                        IsSearchable = true,
                        VectorSearchDimensions = modelDimensions,
                        VectorSearchProfileName = vectorSearchProfile
                    },
                    new SearchableField("Category") { IsFilterable = true, IsSortable = true, IsFacetable = true },
                    new SearchField(
                        "CategoryVector",
                        SearchFieldDataType.Collection(SearchFieldDataType.Single))
                    {
                        IsSearchable = true,
                        VectorSearchDimensions = modelDimensions,
                        VectorSearchProfileName = vectorSearchProfile
                    }
                },
                VectorSearch = new VectorSearch
                {
                    Profiles = { new VectorSearchProfile(vectorSearchProfile, vectorSearchKnnConfig) },
                    Algorithms =
                    {
                        new HnswAlgorithmConfiguration(vectorSearchHnswConfig),
                        new ExhaustiveKnnAlgorithmConfiguration(vectorSearchKnnConfig)
                    }
                }
            };
            var createResponse = await _searchIndexClient.CreateOrUpdateIndexAsync(
                searchIndex,
                allowIndexDowntime: false,
                onlyIfUnchanged: true,
                cancellationToken: cancellationToken);

            var hotelDocuments = await GetHotelDocumentsAsync(cancellationToken);
            var searchClient = _searchClientFactory.CreateForIndex(IndexName);
            var indexResponse = await searchClient.IndexDocumentsAsync(
                IndexDocumentsBatch.Upload(hotelDocuments),
                cancellationToken: cancellationToken);
        }

        private async Task<Hotel[]> GetHotelDocumentsAsync(CancellationToken cancellationToken)
        {
            var hotel1Description =
                "Best hotel in town if you like luxury hotels. They have an amazing infinity pool, a spa, " +
                "and a really helpful concierge. The location is perfect -- right downtown, close to all " +
                "the tourist attractions. We highly recommend this hotel.";
            var hotel1DescriptionVector =
                await _embeddingModel.GetEmbeddingsForTextAsync(hotel1Description, cancellationToken);
            var hotel1Category = "Luxury";
            var hotel1CategoryVector =
                await _embeddingModel.GetEmbeddingsForTextAsync(hotel1Category, cancellationToken);
            var hotel2Description = "Cheapest hotel in town. In fact, a motel.";
            var hotel2DescriptionVector =
                await _embeddingModel.GetEmbeddingsForTextAsync(hotel2Description, cancellationToken);
            var hotel2Category = "Budget";
            var hotel2CategoryVector =
                await _embeddingModel.GetEmbeddingsForTextAsync(hotel2Category, cancellationToken);
            return
            [
                new Hotel
                {
                    HotelId = "1",
                    HotelName = "Fancy Stay",
                    Description = hotel1Description,
                    DescriptionVector = hotel1DescriptionVector,
                    Category = hotel1Category,
                    CategoryVector = hotel1CategoryVector
                },
                new Hotel
                {
                    HotelId = "2",
                    HotelName = "Roach Motel",
                    Description = hotel2Description,
                    DescriptionVector = hotel2DescriptionVector,
                    Category = hotel2Category,
                    CategoryVector = hotel2CategoryVector
                }
                // Add more hotel documents here...
            ];
        }

        private class Hotel
        {
            public string HotelId { get; set; }
            public string HotelName { get; set; }
            public string Description { get; set; }
            public IReadOnlyList<float> DescriptionVector { get; set; }
            public string Category { get; set; }
            public IReadOnlyList<float> CategoryVector { get; set; }
        }
    }
}
