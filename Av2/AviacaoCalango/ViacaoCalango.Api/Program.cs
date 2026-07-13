using AviacaoCalango.Application.Services;
using AviacaoCalango.Domain.Repositories;
using AviacaoCalango.Infrastructure.Persistence;
using AviacaoCalango.Infrastructure.Repositories;
using System.Reflection;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

// DbContext (SQLite arquivo)
builder.Services.AddDbContext<ViacaoCalangoDbContext>(options =>
{
    options.UseSqlite("Data Source=viacao-calango.db");
});

// Repositórios
builder.Services.AddScoped<IOnibusRepository, OnibusRepository>();
builder.Services.AddScoped<IMotoristaRepository, MotoristaRepository>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<IViagemRepository, ViagemRepository>();
builder.Services.AddScoped<IPassagemRepository, PassagemRepository>();

// Application services
builder.Services.AddScoped<VendaAppService>();
builder.Services.AddScoped<EscalaMotoristaAppService>();
builder.Services.AddScoped<CalculadoraPrecoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();



