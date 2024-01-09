namespace Api.Features.Rag.Models
{
    public record EntityResponse(string Id, string Name, string Description, string Author, int Year, double? Score);
}
