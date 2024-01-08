using Api.Azure.Search;
using Api.Features.Rag;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Generator
{
    internal class VectorDb : BackgroundService
    {
        private const string IndexName = "hotels";
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VectorDb> _logger;

        public VectorDb(IServiceScopeFactory scopeFactory, ILogger<VectorDb> logger, IHostApplicationLifetime applicationLifetime)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _applicationLifetime = applicationLifetime;
        }

        private async Task PopulateIndexAsync(CancellationToken cancellationToken)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;
            // create index
            const string VectorSearchProfile = "my-vector-profile";
            const string VectorSearchHnswConfig = "my-hsnw-vector-config";
            const string VectorSearchKnnConfig = "my-knn-vector-config";
            var embeddingModel = serviceProvider.GetRequiredService<EmbeddingModel>();
            var modelDimensions = await embeddingModel.GetEmbeddingsDimensionsAsync(cancellationToken);
            SearchIndex searchIndex = new(IndexName)
            {
                Fields =
                {
                    new SimpleField(nameof(Entity.Id), SearchFieldDataType.String)
                    {
                        IsKey = true, IsFilterable = true, IsSortable = true, IsFacetable = true
                    },
                    new SearchableField(nameof(Entity.Name)) { IsFilterable = true, IsSortable = true },
                    new SearchableField(nameof(Entity.Description)) { IsFilterable = true },
                    new SearchField(
                        nameof(Entity.DescriptionVector),
                        SearchFieldDataType.Collection(SearchFieldDataType.Single))
                    {
                        IsSearchable = true,
                        VectorSearchDimensions = modelDimensions,
                        VectorSearchProfileName = VectorSearchProfile
                    },
                    new SearchableField(nameof(Entity.Category))
                    {
                        IsFilterable = true, IsSortable = true, IsFacetable = true
                    },
                    new SearchField(
                        nameof(Entity.CategoryVector),
                        SearchFieldDataType.Collection(SearchFieldDataType.Single))
                    {
                        IsSearchable = true,
                        VectorSearchDimensions = modelDimensions,
                        VectorSearchProfileName = VectorSearchProfile
                    }
                },
                VectorSearch = new VectorSearch
                {
                    Profiles = { new VectorSearchProfile(VectorSearchProfile, VectorSearchKnnConfig) },
                    Algorithms =
                    {
                        new HnswAlgorithmConfiguration(VectorSearchHnswConfig),
                        new ExhaustiveKnnAlgorithmConfiguration(VectorSearchKnnConfig)
                    }
                }
            };
            var searchIndexClient = serviceProvider.GetRequiredService<SearchIndexClient>();
            await searchIndexClient.CreateOrUpdateIndexAsync(
                index: searchIndex,
                allowIndexDowntime: false,
                onlyIfUnchanged: true,
                cancellationToken: cancellationToken);

            var hotelDocuments = await GetHotelDocumentsAsync(embeddingModel, cancellationToken);
            var searchClientFactory = serviceProvider.GetRequiredService<SearchClientFactory>();
            var searchClient = searchClientFactory.CreateForIndex(IndexName);
            await searchClient.IndexDocumentsAsync(
                IndexDocumentsBatch.Upload(hotelDocuments),
                new IndexDocumentsOptions { ThrowOnAnyError = true },
                cancellationToken: cancellationToken);
        }

        private async Task<Entity[]> GetHotelDocumentsAsync(
            EmbeddingModel embeddingModel,
            CancellationToken cancellationToken)
        {
            var hotel1Description =
                "Best hotel in town if you like luxury hotels. They have an amazing infinity pool, a spa, " +
                "and a really helpful concierge. The location is perfect -- right downtown, close to all " +
                "the tourist attractions. We highly recommend this hotel.";
            var hotel1DescriptionVector =
                await embeddingModel.GetEmbeddingsForTextAsync(hotel1Description, cancellationToken);
            var hotel1Category = "Luxury";
            var hotel1CategoryVector =
                await embeddingModel.GetEmbeddingsForTextAsync(hotel1Category, cancellationToken);
            var hotel2Description = "Cheapest hotel in town. In fact, a motel.";
            var hotel2DescriptionVector =
                await embeddingModel.GetEmbeddingsForTextAsync(hotel2Description, cancellationToken);
            var hotel2Category = "Budget";
            var hotel2CategoryVector =
                await embeddingModel.GetEmbeddingsForTextAsync(hotel2Category, cancellationToken);
            return
            [
                new Entity
                {
                    Id = "1",
                    Name = "Fancy Stay",
                    Description = hotel1Description,
                    DescriptionVector = hotel1DescriptionVector,
                    Category = hotel1Category,
                    CategoryVector = hotel1CategoryVector
                },
                new Entity
                {
                    Id = "2",
                    Name = "Roach Motel",
                    Description = hotel2Description,
                    DescriptionVector = hotel2DescriptionVector,
                    Category = hotel2Category,
                    CategoryVector = hotel2CategoryVector
                }
                // Add more hotel documents here...
            ];
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await PopulateIndexAsync(stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while populating index {IndexName}", IndexName);
            }
            finally
            {
                _applicationLifetime.StopApplication();
            }
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
