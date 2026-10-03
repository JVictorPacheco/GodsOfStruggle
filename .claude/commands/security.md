# /security — Security Review (GodsOfStruggle)

Revise o código apontado com foco em segurança. Execute sempre antes de abrir PR para develop.

## 1. Autorização e IDOR

### IDOR (Insecure Direct Object Reference)
- [ ] Recursos user-scoped (Cart, Orders, Addresses) filtram por `currentUserId` do token JWT — nunca aceitam IDs externos sem verificação de propriedade
- [ ] Handlers que acessam dados de um usuário chamam `ICurrentUserService.UserId`, não parâmetros do request
- [ ] Nenhum endpoint retorna dados de outro usuário se o chamador não for admin

### Autorização por role
- [ ] Endpoints de escrita em recursos públicos (produtos, categorias) verificam `IsAdmin` via `ICurrentUserService`
- [ ] `[Authorize]` presente em TODOS os endpoints que modificam dados
- [ ] Endpoints de leitura pública (listagem de produtos) não exigem auth — correto e intencional
- [ ] Role Admin não é inferida por convenção — é validada explicitamente

### Padrão para recursos user-scoped
```csharp
// Handler CORRETO — usa currentUserId do token
public async Task<CartDto> Handle(GetCartQuery request, ...)
{
    Guid userId = _currentUserService.UserId;
    Cart cart = await _cartRepository.GetByUserIdAsync(userId, ct);
    ...
}

// ERRADO — aceita cartId do cliente, permite IDOR
public async Task<CartDto> Handle(GetCartQuery request, ...)
{
    Cart cart = await _cartRepository.GetByIdAsync(request.CartId, ct);
    ...
}
```

## 2. Validação de Input

- [ ] Todos os commands com dados de entrada têm `FluentValidation`
- [ ] Quantidades numéricas têm limite superior (`quantity > 0 && quantity <= 99`)
- [ ] GUIDs de referência cruzada (ex: `ProductVariantId`) são validados como existentes no handler
- [ ] Strings têm `MaximumLength` definido no validator
- [ ] Decimais de preço têm `GreaterThanOrEqualTo(0)` e `LessThan(1_000_000)`

## 3. Exposição de Dados

- [ ] Respostas de erro não expõem stack traces (ExceptionMiddleware oculta detalhes em produção)
- [ ] Nenhuma propriedade sensível (senha, JWT secret, connection string) em DTOs ou logs
- [ ] `ProductVariant.StockQuantity` não exposto em endpoints públicos de listagem
- [ ] Erros de "não encontrado" retornam 404 sem discriminar se o recurso não existe ou não pertence ao usuário

## 4. Injeção e SQL

- [ ] Nenhuma query SQL raw concatenada com input do usuário — somente EF Core ou parâmetros nomeados
- [ ] Filtros de busca por string usam `.Contains()` ou `.EF.Functions.ILike()` — nunca interpolação
- [ ] Sem `ExecuteSqlRaw` com dados do usuário

## 5. JWT e Autenticação

- [ ] Token JWT é validado pelo middleware antes de chegar nos controllers (configurado no `Program.cs`)
- [ ] `ICurrentUserService.UserId` lê do `ClaimTypes.NameIdentifier` do token — não do body do request
- [ ] Endpoints protegidos retornam 401 sem token, não 403 ou 404

## Output

Liste cada problema com:
- **Arquivo:linha** — descrição
- **Risco** — IDOR / AuthZ / Injection / DataExposure
- **Correção** — como resolver

Se nenhum problema: "APROVADO — sem vulnerabilidades detectadas."
