# Lyfegen Value Driven Contracts - Demo App

Full-stack sample for value-based contracts, structured with Clean Architecture (Api, Application, Domain, Infrastructure).

## Tech and layout
- Server: lyfegen-contracts-api
  - Api: ASP.NET Core 9 with ProblemDetails
  - Application: use cases, mapping and FluentValidation
  - Domain: entities
  - Infrastructure: EF Core with Npgsql, repositories, migrations
  - Tests: xUnit
- Client: lyfegen-contracts-client
  - React 19 + TypeScript, Vite, React Router
  - Tests: Vitest
