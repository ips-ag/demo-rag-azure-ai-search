# Retrieval Augmented Generation (RAG) in Azure AI Search
Demo implementation of Retrieval Augmented Generation (RAG) framework using Azure OpenAI embeddings and Azure AI Search. Based on
https://github.com/webmasterdevlin/vector-database-presentation

## How to use
### Configuration
Before running any application, you need to set the following environment variables:
* **AZURE__OPENAI__APIKEY** - Azure OpenAI service API key
* **AZURE__OPENAI__ENDPOINT** - Azure OpenAI service endpoint URL
* **AZURE__OPENAI__COMPLETION__DEPLOYMENTNAME** - Azure OpenAI deployment name, used to generate completions, e.g, using _gpt-35-turbo_ or _gpt-4_ model
* **AZURE__OPENAI__EMBEDDING__DEPLOYMENTNAME** - Azure OpenAI deployment name, used to generate embeddings, e.g., using _text-embedding-ada-002_ model
* **AZURE__AISEARCH__APIKEY** - Azure AI Search API key
* **AZURE__AISEARCH__ENDPOINT** - Azure AI Search endpoint URL

Alternative is to use `dotnet user-secrets set` command to set the values. Following configuration keys need to be set:
* Azure:OpenAI:ApiKey
* Azure:OpenAI:Endpoint
* Azure:OpenAI:Completion:DeploymentName
* Azure:OpenAI:Embedding:DeploymentName
* Azure:AiSearch:ApiKey
* Azure:AiSearch:Endpoint

### Data preparation

Run **generator** application to create and populate Azure AI Search index. The application reads book data from `data/books.csv` file and generates documents for Azure AI Search. The application uses Azure OpenAI to generate embeddings of vector searchable fields, and stores them in the index.

To start the generator application, run the following command:
```shell
dotnet run --project .\src\generator
```

Depending on the number of books in the dataset and Azure AI Search SKU, the process may take a while. After the process is finished, the application will close. In case of any issuse, the application will log the error and exit.

### Using application
Run **app** application to start the web server. The application provides a simple web interface to search for books, based on user input, and get recommendation. The application uses Azure AI Search vector search to query book descriptions and Azure OpenAI to generate response to the user query.

To start the main application, run the following command:
```shell
dotnet run --project .\src\app
```

## Useful links
* https://learn.microsoft.com/en-us/azure/search/retrieval-augmented-generation-overview
* https://learn.microsoft.com/en-us/azure/ai-services/openai/how-to/embeddings
* https://learn.microsoft.com/en-us/azure/search/vector-search-how-to-create-index
* https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/search/Azure.Search.Documents/samples/Sample07_VectorSearch_UsingRawVectorQuery.md
* https://www.kaggle.com/datasets/dylanjcastillo/7k-books-with-metadata
