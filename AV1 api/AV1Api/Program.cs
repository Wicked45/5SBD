using Microsoft.EntityFrameworkCore;
using AV1Api.Infrastructure;
using AV1Api.Application;
using AV1Api.API;
using ViacaoCalangoApi.Application;
using ViacaoCalangoApi.Domain;
using ViacaoCalangoApi.Infrastructure;
using ViacaoCalangoApi.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mantém o DbContext original do AV1
builder.Services.AddDbContext<BazarDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("Postgres")
                   ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

    options.UseNpgsql(connStr);
});

builder.Services.AddScoped<IBazarRepository, BazarRepository>();
builder.Services.AddScoped<IBazarAppService, BazarAppService>();

// DbContext DDD do Viação Calango (InMemory)
builder.Services.AddDbContext<CalangoDbContext>(options =>
    options.UseInMemoryDatabase("CalangoDB"));

builder.Services.AddScoped<ICalangoRepository, CalangoRepository>();
builder.Services.AddScoped<ICalangoAppService, CalangoAppService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();



