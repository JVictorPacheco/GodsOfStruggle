# GodsOfStruggle — PROGRESS.md

## Status Geral
**Prazo:** 02/10/2026 (apresentação ao cliente)
**Branch ativa:** feature/GOS-003-products → develop → main

---

## Features Concluídas

### GOS-001 — Domain ✅
**Branch:** feature/GOS-001-domain (mergeada em develop)
- `BaseEntity`, `DomainEvent` (base classes)
- Value objects: `Money`, `ShippingAddress`
- Entidades ricas: `Product`, `ProductVariant`, `ProductImage`, `Category`, `Order`, `OrderItem`, `Cart`, `CartItem`, `User`, `Address`
- Enums: `OrderStatus`, `PaymentStatus`, `PaymentMethod`, `UserRole`
- Exceções de domínio: `DomainException`, `InsufficientStockException`, `InvalidOrderStatusTransitionException`

### GOS-002 — Auth (Supabase JWT) ✅
**Branch:** feature/GOS-002-auth (mergeada em develop)
- JWT Bearer validation (.NET) com secret Base64 do Supabase
- `SyncSupabaseUser` command — cria/atualiza user no primeiro login
- `GetCurrentUser` query
- `ValidationBehavior` pipeline (FluentValidation + MediatR)
- `CurrentUserService` (IHttpContextAccessor)
- `AppDbContext` + `IDesignTimeDbContextFactory`
- Configurações EF: `UserConfiguration`, `AddressConfiguration`
- Swagger com suporte JWT (Swashbuckle 6.9.0)
- CORS configurado para `http://localhost:3000`

### GOS-003 — Products ✅
**Branch:** feature/GOS-003-products (em andamento, pendente merge)
**Backend (.NET):**
- Commands: `CreateProduct`, `UpdateProduct`, `DeleteProduct`, `AddProductVariant`, `UpdateStock`
- Queries: `GetProducts` (paginado, filtros: search/categoryId/minPrice/maxPrice), `GetProductById`
- DTOs: `ProductDto`, `ProductListItemDto`, `ProductVariantDto`, `ProductImageDto`, `PagedResult<T>`
- `ProductMapper` (static ToDto / ToListItemDto)
- `ProductRepository` com filtros IQueryable + paginação
- `ProductConfiguration`, `ProductVariantConfiguration`, `ProductImageConfiguration` (EF)
- Coluna `Attributes` como JSONB (Dictionary<string,string>)
- `BasePrice` e `Price` como OwnsOne (Money value object)

**Frontend (Next.js 16):**
- `types/index.ts` — interfaces TypeScript completas
- `lib/api/client.ts`, `lib/api/products.ts`, `lib/api/categories.ts`
- Páginas: home (`/`), listagem (`/products`), detalhe (`/products/[id]`)
- Componentes: `ProductCard`, `CategoryFilter`
- Tema visual alinhado ao logotipo (fundo preto, laranja #f97316, azul #3b82f6)
- Logo inserida no header

**Banco de Dados:**
- Tabelas criadas via SQL no Supabase SQL Editor (AppLocker impede `dotnet ef database update`)
- Seed data: 4 categorias, 8 produtos, 37 variantes, 8 imagens (Unsplash)
- Inserção feita via Supabase REST API (sem psql)
- Coluna naming: PascalCase (EF sem convenção snake_case), exceto `base_price`, `price_currency`, `price`

---

## Infra / DevOps Resolvidos

### AppLocker (Windows WDAC)
- `dotnet run` bloqueado de `bin\Debug` e `bin\Release`
- **Solução:** `dotnet publish -c Release -o "C:\ProgramData\GodsOfStruggle\api\"`
- **Iniciar API:** 
  ```powershell
  Set-Location "C:\Users\Camarj\projects\GodsOfStruggle\backend\GodsOfStruggle.API"
  $env:ASPNETCORE_ENVIRONMENT = "Development"
  $env:ASPNETCORE_URLS = "http://localhost:5270"
  & "C:\ProgramData\GodsOfStruggle\api\GodsOfStruggle.API.exe"
  ```

### Supabase Conectividade
- Host direto (`db.noahvgfsjtjlcshxkbjt.supabase.co`) só tem IPv6 — sem conectividade TCP
- **Solução:** Usar session-mode pooler: `aws-0-us-east-2.pooler.supabase.com:5432`
- Username: `postgres.noahvgfsjtjlcshxkbjt`

### Ports
- API: `http://localhost:5270`
- Frontend: `http://localhost:3000`

---

## Próximas Features

| ID | Feature | Status |
|---|---|---|
| GOS-004 | Cart — backend + frontend | Pendente |
| GOS-005 | Checkout mock | Pendente |
| GOS-006 | AI mock (busca semântica, chatbot, recomendações) | Pendente |
| GOS-007 | Admin dashboard mock | Pendente |

---

## Decisões Técnicas Importantes

- **AI mockada:** Claude Pro (subscription) ≠ Anthropic API Key — features de IA serão mockadas no protótipo
- **Commit strategy:** 1 commit por camada lógica (Application / Infrastructure / API / Frontend / DB)
- **Swashbuckle:** versão 6.9.0 (não 10.x — conflito com Microsoft.OpenApi 3.x)
- **EF DomainEvents:** `OnModelCreating` faz `.Ignore(nameof(DomainEvents))` em todos os subtipos de `BaseEntity`
- **JWT:** `Convert.FromBase64String(jwtSecret)` — o secret do Supabase é Base64-encoded
- **Seed data inserido via:** Supabase REST API (PostgREST) com service role key — colunas PascalCase
