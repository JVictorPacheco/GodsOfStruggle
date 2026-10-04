# GodsOfStruggle — Progress Tracker

## Status Atual
**Fase:** GOS-004 concluído e mergeado — próximo: GOS-005 (Auth frontend)
**Branch ativa:** `develop`
**Última atualização:** 2026-10-04

---

## Concluído

### Setup (29-30/09)
- [x] Spec v0.1 validado (`.claude/SPEC_v0.1.md`)
- [x] Repositório GitHub: https://github.com/JVictorPacheco/GodsOfStruggle
- [x] Git Flow: `main` + `develop`
- [x] Solução .NET 10 — Clean Architecture (Domain, Application, Infrastructure, API, Tests)
- [x] Next.js 16 + TypeScript + Tailwind CSS + shadcn/ui
- [x] CLAUDE.md, skills `/spec` `/feature` `/gitflow` `/review` `/security` `/migration`
- [x] Supabase configurado (PostgreSQL pooler: aws-0-us-east-2.pooler.supabase.com:5432)

### GOS-001 — Domínio (30/09)
- [x] Entidades: Product, ProductVariant, ProductImage, Category, User, Address, Cart, CartItem, Order, OrderItem
- [x] Value Objects: Money (Amount + Currency), com Add() que valida moeda
- [x] Enums: OrderStatus, PaymentStatus, UserRole
- [x] Exceptions: DomainException, NotFoundException
- [x] Interfaces: IRepository<T>, ICurrentUserService
- [x] Result<T> pattern para erros de domínio

### GOS-002 — Auth (01/10)
- [x] JWT Bearer validado contra Supabase (issuer = Supabase project URL)
- [x] CurrentUserService (lê UserId + IsAdmin do JWT)
- [x] AuthController: POST /api/v1/auth/sync (sincroniza usuário Supabase → DB local)
- [x] UserController: GET /api/v1/users/me
- [x] EF Core + Npgsql configurado (session pooler Supabase)
- [x] Migration inicial: tabelas users, addresses
- [x] Seed SQL: categorias + produtos com variantes + imagens

### GOS-003 — Products (01-02/10) — PR #1 mergeado
- [x] CQRS completo: GetProducts, GetProductById, CreateProduct, UpdateProduct, DeleteProduct, AddVariant, UpdateStock
- [x] Validators FluentValidation em todos os commands
- [x] ProductsController com admin guard (ICurrentUserService.IsAdmin)
- [x] CategoriesController: GET /api/v1/categories
- [x] Frontend: listagem /products + detalhe /products/[id] com seletor de variante
- [x] Tema visual: dark (#0a0a0a), laranja (#f97316), tipografia Geist
- [x] Code review (9 findings resolvidos): Result<T>, NotFoundException, validators, Money.Add()
- [x] Skills adicionadas: `/security`, `/migration`

### GOS-004 — Cart (03-04/10) — PR #2 mergeado
- [x] CQRS: GetCart (IDOR-safe), AddToCart, RemoveFromCart, ClearCart
- [x] CartController [Authorize], 4 endpoints REST
- [x] CartRepository com GetOrCreateByUserIdAsync (lazy create)
- [x] EF Core: OwnsOne Money (colunas price, price_currency)
- [x] Migration SQL aplicada no Supabase: tabelas carts + cart_items
- [x] Frontend: CartContext, CartNav (badge laranja), /cart page, botão "Adicionar ao Carrinho"
- [x] Fix: alias CartEntity para resolver CS0118 (namespace Cart vs. tipo Cart)

---

## Próximo — GOS-005: Auth Frontend

**Objetivo:** Login e cadastro via Supabase Auth, obter JWT e passar para API.

### O que precisa ser feito
- [ ] Página `/login` com form email + senha (Supabase Auth)
- [ ] Página `/register` com form (Supabase Auth)
- [ ] Armazenar sessão Supabase (`supabase.auth.getSession()`)
- [ ] Passar token JWT nas chamadas à API (header `Authorization: Bearer <token>`)
- [ ] Wiring do token no `CartContext` (hoje passa `""` — precisa do token real)
- [ ] `AuthContext` ou hook `useAuth` para expor `user`, `session`, `signIn`, `signOut`
- [ ] Redirecionar para /login em rotas protegidas (/cart, checkout futuro)
- [ ] Botão Login/Logout no header

### Após GOS-005 (backlog)
- [ ] GOS-006 — Checkout mock (Order + OrderItems, sem gateway real)
- [ ] GOS-007 — IA: busca semântica (pgvector), recomendações, chatbot
- [ ] GOS-008 — Admin dashboard com insights IA

---

## Arquitetura — Referência Rápida

```
Domain ← Application ← Infrastructure ← API
```

| Camada | Regra |
|---|---|
| Domain | Zero dependências externas. Entidades ricas. |
| Application | Só interfaces. Handlers MediatR. Result<T> para erros. |
| Infrastructure | EF Core, Npgsql, Supabase Auth, Anthropic SDK |
| API | Controllers finos. Só MediatR + HTTP response. |

## Workarounds Conhecidos

| Problema | Solução |
|---|---|
| AppLocker bloqueia `dotnet run` em projetos | Publicar em `C:\ProgramData\GodsOfStruggle\api\` e rodar de lá |
| AppLocker bloqueia `dotnet ef` | Gerar SQL manual + aplicar no Supabase SQL Editor (ou via `C:\ProgramData\GodsOfStruggle\migrator\`) |
| Supabase IPv6 (direct host) | Usar pooler: `aws-0-us-east-2.pooler.supabase.com:5432` |
| API precisa de env vars | Rodar com `ASPNETCORE_ENVIRONMENT=Development` (carrega appsettings.Development.json) |
| PowerShell heredoc para git/gh | Usar `@'...'@` + gravar em arquivo temporário para `--body-file` |

## Decisões de Design

- **Coluna naming:** PascalCase (padrão EF) — exceto money: `price`, `price_currency`
- **IDOR prevention:** handlers user-scoped nunca aceitam userId do request — sempre `ICurrentUserService.UserId`
- **Commands retornam Guid** (não DTO completo)
- **Token no frontend:** hoje hardcoded `""` em `cart.ts` — será substituído pelo token Supabase em GOS-005
