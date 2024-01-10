#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Api.Features.Core.VectorDb.Models
{
    public class Entity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public ReadOnlyMemory<float> DescriptionVector { get; set; }
    }
}
