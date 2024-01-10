using Api.Azure.OpenAi.Extensions;
using Api.Azure.Search.Extensions;
using Api.Features.Rag.Extensions;
using Generator.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Generator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += (_, eventArgs) =>
            {
                eventArgs.Cancel = true;
                // ReSharper disable once AccessToDisposedClosure
                cancellationTokenSource.Cancel();
            };
            var builder = Host.CreateDefaultBuilder(args).ConfigureServices(
                services =>
                {
                    services.AddOpenAi().AddAiSearch();
                    services.AddSingleton<IEntityDataSource<Book>, BookDataSource>();
                    services.AddHostedService<VectorDb>();
                });
            var host = builder.Build();
            await host.RunAsync(cancellationTokenSource.Token);
        }
    }
}
