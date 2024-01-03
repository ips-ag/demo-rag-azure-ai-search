using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Api.Azure.OpenAi.Configuration
{
    [DataContract]
    public class OpenAiOptions
    {
        public static string SectionName => "Azure:OpenAI";

        [DataMember(Name = "apiKey")]
        [Required]
        public required string ApiKey { get; set; }

        [DataMember(Name = "endpoint")]
        [Required]
        public required string Endpoint { get; set; }

        [DataMember(Name = "completion")]
        public CompletionOptions? Completion { get; set; }

        [DataMember(Name = "embedding")]
        [Required]
        public required EmbeddingOptions Embedding { get; set; }
    }
}
