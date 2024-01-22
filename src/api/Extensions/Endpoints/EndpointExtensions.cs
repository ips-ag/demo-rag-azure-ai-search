using MediatR;
using Microsoft.AspNetCore.Mvc;
using RagCommands = Api.Features.Rag.Commands;
using RagModels = Api.Features.Rag.Models;
using HybridRagCommands = Api.Features.HybridRag.Commands;
using HybridRagModels = Api.Features.HybridRag.Models;

namespace Api.Extensions.Endpoints
{
    internal static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapFeatureEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost(
                    "/rag",
                    ([FromBody] RagModels.SearchRequest request, [FromServices] IMediator mediator) =>
                    {
                        var command = new RagCommands.SearchCommand(request);
                        return mediator.Send(command);
                    })
                .Produces<RagModels.SearchResponse>()
                .ProducesValidationProblem()
                .WithName("GetResultUsingRag")
                .WithOpenApi();

            app.MapPost(
                    "/hybridrag",
                    ([FromBody] HybridRagModels.SearchRequest request, [FromServices] IMediator mediator) =>
                    {
                        var command = new HybridRagCommands.SearchCommand(request);
                        return mediator.Send(command);
                    })
                .Produces<HybridRagModels.SearchResponse>()
                .ProducesValidationProblem()
                .WithName("GetResultUsingHybridRag")
                .WithOpenApi();

            return app;
        }
    }
}
