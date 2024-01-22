using Api.Azure.OpenAi.Extensions;
using Api.Azure.Search.Extensions;
using Api.Extensions.ExceptionHandler;
using Api.Features.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using RagCommands = Api.Features.Rag.Commands;
using RagModels = Api.Features.Rag.Models;
using HybridRagCommands = Api.Features.HybridRag.Commands;
using HybridRagModels = Api.Features.HybridRag.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds((type) => type.FullName));
builder.Services.AddProblemDetailsExceptionHandler();
builder.Services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/dist");

builder.Services.AddFeatures();
builder.Services.AddOpenAi().AddAiSearch();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseDefaultFiles();
app.UseStaticFiles();

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

app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
    spa.UseReactDevelopmentServer(npmScript: "start");
});

app.Run();
