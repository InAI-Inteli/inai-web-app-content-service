using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPIContentService.Infra.Data.Context;
using WebAPIContentService.Infra.Data.Repository;
using WebAPIContentService.Infra.Data.Repository.Interfaces;
using WebAPIContentService.Infra.Tools;
using WebAPIContentService.Service.Interfaces;
using WebAPIContentService.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<ContentContext>(options =>
{
    var configuration = builder.Configuration;
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
               });

builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialService, MaterialService>();

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

app.Run();
