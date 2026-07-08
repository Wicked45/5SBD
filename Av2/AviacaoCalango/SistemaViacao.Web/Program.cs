using Microsoft.EntityFrameworkCore;
using SistemaViacao.Core.Interfaces;
using SistemaViacao.Data;
using SistemaViacao.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=viacao.db"));

builder.Services.AddScoped<IFrotaRepository, FrotaRepository>();
builder.Services.AddScoped<GestaoFrotaService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

