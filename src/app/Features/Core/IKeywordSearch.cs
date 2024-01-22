using Api.Features.Core.Domain;

namespace Api.Features.Core
{
    internal interface IKeywordSearch
    {
        public Task<IReadOnlyCollection<EntityResponse>> GetByKeywordAsync(
            string query,
            CancellationToken cancellationToken);
    }
}
