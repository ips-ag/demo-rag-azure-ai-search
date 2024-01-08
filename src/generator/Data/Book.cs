using System.Text.Json.Serialization;

namespace Generator.Data
{
    internal class Book
    {
        [JsonPropertyName("name")]
        [JsonRequired]
        public required string Name { get; set; }

        [JsonPropertyName("description")]
        [JsonRequired]
        public required string Description { get; set; }

        [JsonPropertyName("author")]
        [JsonRequired]
        public required string Author { get; set; }

        [JsonPropertyName("year")]
        [JsonRequired]
        public required int Year { get; set; }
    }
}
