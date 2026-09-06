# Career Intelligence Platform

An interview-driven backend project built with .NET and Azure.

The goal of this project is to design and implement a production-oriented backend while practicing the concepts commonly discussed in backend and software engineering interviews.

## Current Stack

- .NET 10
- ASP.NET Core
- C#
- Git / GitHub
- SQL Server
- Entity Framework Core
- xUnit
- Docker
- Kubernetes
- Azure
- Bicep

## Architecture

The solution follows Clean Architecture principles with explicit dependency boundaries.

```text
src/
├── CareerIntelligencePlatform.Api
├── CareerIntelligencePlatform.Application
├── CareerIntelligencePlatform.Domain
└── CareerIntelligencePlatform.Infrastructure

tests/
├── CareerIntelligencePlatform.Domain.Tests
├── CareerIntelligencePlatform.Application.Tests
├── CareerIntelligencePlatform.Infrastructure.Tests
└── CareerIntelligencePlatform.Api.Tests
```
