# Retrieval Augmented Generation (RAG) in Azure AI Search
Demo implementation of Retrieval Augmented Generation (RAG) framework using Azure OpenAI embeddings and Azure AI Search. Based on
https://github.com/webmasterdevlin/vector-database-presentation

## How to use
### Configuration
Before running any application, you need to set the following environment variables:
* Azure__OpenAI__ApiKey - Azure OpenAI service API key
* Azure__OpenAI__Endpoint - Azure OpenAI service endpoint URL
* Azure__OpenAI__Completion__DeploymentName - Azure OpenAI deployment name, used to generate completions, e.g, using _gpt-35-turbo_ or _gpt-4_ model
* Azure__OpenAI__Embedding__DeploymentName - Azure OpenAI deployment name, used to generate embeddings, e.g., using _text-embedding-ada-002_ model
* Azure__AiSearch__ApiKey - Azure AI Search API key
* Azure__AiSearch__Endpoint - Azure AI Search endpoint URL

Alternative is to use `dotnet user-secrets set` command to set the values. Following configuration keys need to be set:
* Azure:OpenAI:ApiKey
* Azure:OpenAI:Endpoint
* Azure:OpenAI:Completion:DeploymentName
* Azure:OpenAI:Embedding:DeploymentName
* Azure:AiSearch:ApiKey
* Azure:AiSearch:Endpoint

## Useful links
* https://learn.microsoft.com/en-us/azure/search/retrieval-augmented-generation-overview
* https://learn.microsoft.com/en-us/azure/ai-services/openai/how-to/embeddings
* https://learn.microsoft.com/en-us/azure/search/vector-search-how-to-create-index
* https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/search/Azure.Search.Documents/samples/Sample07_VectorSearch_UsingRawVectorQuery.md
* https://www.kaggle.com/datasets/dylanjcastillo/7k-books-with-metadata
