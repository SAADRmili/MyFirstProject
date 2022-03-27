using FluentMigrator.Runner;
using MyFirstProject.API.Contexts;
using MyFirstProject.API.Extensions;
using MyFirstProject.API.Migrations;
using System.Reflection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<dbContext>();
builder.Services.AddSingleton<Database>();


builder.Services.AddLogging(c=> c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("dbConnection"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.Run();