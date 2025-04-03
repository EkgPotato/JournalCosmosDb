using FluentValidation;
using WebApi.AppStartup.GroupEndpoints;
using WebApi.AppStartup.Validators;
using WebApi.Interfaces;
using WebApi.Mapper;
using WebApi.Models.DTOs;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddUserSecrets<Program>();

builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddAutoMapper(e => e.AddProfile<EntryMapper>());
builder.Services.AddScoped<IValidator<EntryDTO>, EntryValidator>();
builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();
builder.Services.AddScoped<IEntryService, EntryService>();

var app = builder.Build();

app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("/entries")
    .MapEntriesApi();


app.UseHttpsRedirection();

app.Run();
