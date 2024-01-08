using System.Text;
using Api.Features.Rag.Models;

namespace Api.Features.Rag
{
    internal class PromptFactory
    {
        public string CreateFromSearchResults(string requestPrompt, IEnumerable<EntityResponse> searchResults)
        {
            var bestResult = searchResults.FirstOrDefault();
            var sb = new StringBuilder("Act as a search copilot, be helpful and informative.");
            sb.AppendLine("--------------");
            sb.AppendLine("Based on the user's query below: ");
            sb.AppendLine($"'{requestPrompt}'");
            sb.AppendLine("Here is some information about the query. It has the following information:");
            if (bestResult is null)
            {
                sb.AppendLine("No information found");
            }
            else
            {
                sb.AppendLine($"Name is {bestResult.Name}");
                sb.AppendLine($"Description is '{bestResult.Description}'");
                sb.AppendLine($"Author is '{bestResult.Author}'");
                sb.AppendLine($"Year is '{bestResult.Year}'");
            }
            sb.AppendLine("--------------");
            sb.AppendLine("note: Be concise and dont add any other details if you don't know about it.");
            return sb.ToString();
        }
    }
}
