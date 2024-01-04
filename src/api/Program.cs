using Api.Azure.OpenAi.Extensions;
using Api.Azure.Search.Extensions;
using Api.Extensions.ExceptionHandler;
using Api.Features.Extensions;
using Api.Features.Rag.Commands;
using Api.Features.Rag.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetailsExceptionHandler();

builder.Services.AddOpenAi().AddAiSearch();
builder.Services.AddFeatures();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.MapPost(
        "/rag/base",
        ([FromBody] SearchRequest request, [FromServices] IMediator mediator) =>
        {
            var command = new SearchCommand(request);
            return mediator.Send(command);
        })
    .Produces<SearchResponse>()
    .ProducesValidationProblem()
    .WithName("GetResultUsingBaseRag")
    .WithOpenApi();

app.Run();
