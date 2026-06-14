# AV1Api

API REST .NET (DDD + EF Core + PostgreSQL/Npgsql) para expor dados e acionar procedures.

## Endpoints

- GET /api/bazar/pedidos
- GET /api/bazar/reposicao
- POST /api/bazar/importar-etl
- POST /api/bazar/processar-estoque

## Configuração

Edite `appsettings.json` com a ConnectionString `ConnectionStrings:Postgres`.
