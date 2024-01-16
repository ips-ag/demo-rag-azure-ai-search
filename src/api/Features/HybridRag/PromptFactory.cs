using System.Text;
using Api.Features.Core.Domain;

namespace Api.Features.HybridRag
{
    internal class PromptFactory
    {
        public string CreateFromSearchResults(string requestPrompt, IReadOnlyCollection<EntityResponse> searchResults)
        {
            var sb = new StringBuilder("Act as a search copilot, be helpful and informative.");
            sb.AppendLine("--------------");
            sb.AppendLine("Based on the user's query below: ");
            sb.AppendLine($"'{requestPrompt}'");
            sb.AppendLine("Here is some information about the query. It has the following information:");
            if (searchResults.Count == 0)
            {
                sb.AppendLine("No information found");
            }
            else
            {
                sb.AppendLine("Choose only one best match from the following:");
                foreach (var searchResult in searchResults)
                {
                    sb.AppendLine(
                        $"* Name is '{searchResult.Name}', Description is '{searchResult.Description}', Author is '{searchResult.Authors}',Year is '{searchResult.Year}'");
                }
            }
            sb.AppendLine("--------------");
            sb.AppendLine("note: Be concise and dont add any other details if you don't know about it.");
            return sb.ToString();
        }
    }
}
