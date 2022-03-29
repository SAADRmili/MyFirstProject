using FluentMigrator.Runner;
using MyFirstProject.API.Contexts;
using MyFirstProject.API.Extensions;
using MyFirstProject.API.Migrations;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using MyFirstProject.Shared.Services;
using MyFirstProject.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddSingleton<dbContext>();
builder.Services.AddSingleton<Database>();


builder.Services.AddLogging(c=> c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres()
            .WithGlobalConnectionString(builder.Configuration.GetConnectionString("dbConnection"))
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());;




builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IBrandService, BrandService>();


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
