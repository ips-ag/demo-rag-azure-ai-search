using Api.Azure.OpenAi.Extensions;
using Api.Azure.Search.Extensions;
using Api.Extensions.Endpoints;
using Api.Extensions.ExceptionHandler;
using Api.Features.Extensions;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName));
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

app.MapFeatureEndpoints();

app.UseSpaStaticFiles();

app.UseSpa(
    spa =>
    {
        spa.Options.SourcePath = "ClientApp";
        spa.Options.DevServerPort = 5173;
        if (app.Environment.IsDevelopment()) spa.UseReactDevelopmentServer(npmScript: "dev");
    });

app.Run();
