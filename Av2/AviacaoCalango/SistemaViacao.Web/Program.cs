using Microsoft.EntityFrameworkCore;
using SistemaViacao.Core.Interfaces;
using SistemaViacao.Data;
using SistemaViacao.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Adicionar os serviços ao container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=viacao.db"));

// Injeção de Dependência (Padrão Repository)
builder.Services.AddScoped<IFrotaRepository, FrotaRepository>();
builder.Services.AddScoped<IVendasRepository, VendasRepository>(); // Adicionado para Vendas!

// Injeção de Dependência (Use Cases/Services)
builder.Services.AddScoped<GestaoFrotaService>();
builder.Services.AddScoped<VendasService>(); // Adicionado para Vendas!

var app = builder.Build();

// Garantir que o banco é criado ao inicializar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configurar o pipeline de requisições HTTP 
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();