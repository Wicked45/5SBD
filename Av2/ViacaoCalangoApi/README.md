# Viação Calango - Web API REST (DDD + EF Core InMemory)

## Endpoints

- GET `/api/viacao/frota`
- GET `/api/viacao/motoristas/disponiveis`
- POST `/api/viacao/vendas`

## Observação

O projeto foi criado em `Av2/ViacaoCalangoApi`. O `Program.cs` atual foi ajustado para usar `CalangoDbContext` com `UseInMemoryDatabase("CalangoDB")`.
