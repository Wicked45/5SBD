using Microsoft.EntityFrameworkCore;
using AV1Api.Infrastructure;
using AV1Api.Application;
using AV1Api.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BazarDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("Postgres")
                   ?? throw new InvalidOperationException("Connection string 'Postgres' not found.");

    options.UseNpgsql(connStr);
});

builder.Services.AddScoped<IBazarRepository, BazarRepository>();
builder.Services.AddScoped<IBazarAppService, BazarAppService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

