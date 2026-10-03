# /feature — Gerador de Feature (Clean Architecture + CQRS)

Você é um arquiteto de software especializado em Clean Architecture, CQRS e DDD.

## Pré-requisitos
- A feature DEVE ter spec validada antes de ser implementada
- Leia o CLAUDE.md e o spec da feature antes de começar
- Se a feature for user-scoped (Cart, Orders, Address), leia `/security` antes de implementar

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

## Features user-scoped (Cart, Orders, Address, Profile)

Quando a feature acessa dados pertencentes a um usuário específico:

1. **Handler usa `ICurrentUserService`** — nunca aceita userId do request body ou query string
2. **Repository filtra por userId** — `GetByUserIdAsync(Guid userId)`, não `GetByIdAsync(Guid id)`
3. **Controller NÃO passa userId** — o handler busca do token JWT via `ICurrentUserService`
4. **Nenhum endpoint expõe cartId/orderId de outros usuários**

```csharp
// Padrão correto para handler user-scoped
public class GetCartQueryHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
    : IRequestHandler<GetCartQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken ct)
    {
        Cart cart = await cartRepository.GetOrCreateByUserIdAsync(currentUser.UserId, ct);
        return CartMapper.ToDto(cart);
    }
}
```

## Ordem de implementação
1. Domain (se necessário) → 2. Application → 3. Infrastructure → 4. API

## Checklist de segurança antes de finalizar
- [ ] Endpoints user-scoped usam `ICurrentUserService.UserId`
- [ ] Todos os commands têm validator
- [ ] Controllers com escrita têm `[Authorize]`
- [ ] Nenhum dado sensível em DTOs de resposta pública
