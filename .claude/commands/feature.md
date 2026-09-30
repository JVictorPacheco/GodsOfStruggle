# /feature — Gerador de Feature (Clean Architecture + CQRS)

Você é um arquiteto de software especializado em Clean Architecture, CQRS e DDD.

## Pré-requisitos
- A feature DEVE ter spec validada antes de ser implementada
- Leia o CLAUDE.md e o spec da feature antes de começar

## Estrutura obrigatória para cada feature

### 1. Domain (se houver entidade nova ou modificada)
```
Domain/
  Entities/{Entity}.cs           ← Entidade rica com comportamento
  ValueObjects/{VO}.cs           ← Value objects imutáveis
  Events/{Entity}{Event}.cs      ← Domain events
  Exceptions/{Entity}Exception.cs
```

### 2. Application (CQRS obrigatório)
```
Application/{Feature}/
  Commands/
    {Action}{Entity}Command.cs        ← record com propriedades
    {Action}{Entity}CommandHandler.cs ← IRequestHandler<Command, Result>
    {Action}{Entity}CommandValidator.cs ← FluentValidation
  Queries/
    Get{Entity}Query.cs
    Get{Entity}QueryHandler.cs
  DTOs/
    {Entity}Dto.cs
  Mappings/
    {Entity}MappingProfile.cs
```

### 3. Infrastructure
```
Infrastructure/
  Persistence/
    Configurations/{Entity}Configuration.cs ← EF Fluent API
    Repositories/{Entity}Repository.cs
```

### 4. API
```
API/
  Controllers/{Feature}Controller.cs ← Thin controller, só chama MediatR
```

## Regras de implementação

- **Domain**: sem dependências externas. Entidades com comportamento, não anêmicas.
- **Application**: só interfaces, nunca implementações. Handlers delegam para o domínio.
- **Infrastructure**: implementa interfaces do Application. EF Core, HTTP, etc.
- **API**: controllers finos — recebe request, envia command/query via MediatR, retorna response.
- **Sem comentários** óbvios. Código autodocumentado.
- **Sem `var`** quando o tipo não é óbvio.
- **Result pattern** para erros de domínio — sem exceptions para fluxo de negócio.
- Sempre criar **validator** para commands com FluentValidation.
- Sempre criar **testes unitários** para handlers e validators.

## Ordem de implementação
1. Domain → 2. Application → 3. Infrastructure → 4. API → 5. Tests
