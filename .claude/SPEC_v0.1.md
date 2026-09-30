# SPEC v0.1 — GodsOfStruggle
**Status:** Aguardando validação  
**Data:** 2026-09-29  
**Versão:** 0.1 — Protótipo

---

## 1. Visão Geral

**Nome:** GodsOfStruggle  
**Propósito:** Plataforma de e-commerce especializada em kimonos e artigos de luta, arquitetada para suportar expansão para qualquer categoria de produto.  
**Público-alvo:** Praticantes de artes marciais (BJJ, Judô) e o administrador/lojista.  
**Idioma/Região:** Português BR, moeda BRL.

---

## 2. Stack Tecnológica

| Camada | Tecnologia | Versão |
|---|---|---|
| Frontend | Next.js (App Router) | 14+ |
| Linguagem Front | TypeScript | 5+ |
| Estilização | Tailwind CSS + shadcn/ui | latest |
| Backend | ASP.NET Core Web API | .NET 9 |
| Linguagem Back | C# | 13 |
| CQRS | MediatR | 12+ |
| ORM | Entity Framework Core | 9+ |
| Banco de Dados | Supabase (PostgreSQL + pgvector) | latest |
| Autenticação | Supabase Auth + JWT | latest |
| IA | Anthropic SDK (.NET + JS) | latest |
| Embeddings/Busca | pgvector (Supabase) | latest |
| Versionamento | Git Flow | — |
| MCP Dev | Context7 + Supabase MCP | latest |

---

## 3. Arquitetura

### 3.1 Princípios
- **Clean Architecture** — separação estrita de camadas
- **SOLID** — cada classe tem responsabilidade única, aberta para extensão
- **CQRS** — separação de Commands (escrita) e Queries (leitura) via MediatR
- **Clean Code** — nomenclatura clara, funções pequenas, sem comentários óbvios
- **DDD light** — entidades ricas, value objects, domain events

### 3.2 Estrutura do Backend

```
GodsOfStruggle.sln
├── src/
│   ├── GodsOfStruggle.Domain/
│   │   ├── Entities/
│   │   ├── ValueObjects/
│   │   ├── Enums/
│   │   ├── Events/
│   │   └── Exceptions/
│   │
│   ├── GodsOfStruggle.Application/
│   │   ├── Products/
│   │   │   ├── Commands/
│   │   │   └── Queries/
│   │   ├── Orders/
│   │   ├── Cart/
│   │   ├── Auth/
│   │   ├── AI/
│   │   ├── Interfaces/
│   │   └── Common/
│   │       ├── Behaviors/        ← ValidationBehavior, LoggingBehavior
│   │       └── Mappings/
│   │
│   ├── GodsOfStruggle.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── Configurations/   ← EF Fluent API
│   │   │   └── Repositories/
│   │   ├── Auth/                 ← Supabase Auth integration
│   │   ├── AI/                   ← Anthropic + pgvector
│   │   └── DependencyInjection/
│   │
│   └── GodsOfStruggle.API/
│       ├── Controllers/
│       ├── Middleware/
│       ├── Filters/
│       └── Program.cs
│
└── tests/
    ├── GodsOfStruggle.Domain.Tests/
    ├── GodsOfStruggle.Application.Tests/
    └── GodsOfStruggle.Integration.Tests/
```

### 3.3 Estrutura do Frontend

```
gods-of-struggle-web/
├── app/
│   ├── (store)/               ← Layout da loja (público)
│   │   ├── page.tsx           ← Home
│   │   ├── products/
│   │   ├── cart/
│   │   ├── checkout/
│   │   ├── orders/
│   │   └── auth/
│   └── (admin)/               ← Layout do painel admin
│       ├── dashboard/
│       ├── products/
│       ├── categories/
│       └── orders/
├── components/
│   ├── ui/                    ← Componentes base (shadcn/ui)
│   ├── store/                 ← ProductCard, CartItem, CheckoutStep
│   └── admin/                 ← DataTable, InsightsCard
├── lib/
│   ├── api/                   ← Cliente HTTP tipado
│   ├── ai/                    ← Hooks de IA (chat, search)
│   └── utils/
└── types/                     ← Tipos TypeScript alinhados com backend
```

---

## 4. Modelo de Domínio

### 4.1 Entidades Principais

#### Product (Produto)
```
Product
├── Id: Guid
├── Name: string
├── Description: string
├── BasePrice: Money (ValueObject)
├── CategoryId: Guid
├── Images: List<ProductImage>
├── Attributes: List<ProductAttributeDefinition>  ← dinâmico (tamanho, cor, etc.)
├── Variants: List<ProductVariant>
├── IsActive: bool
├── Embedding: vector(1536)  ← pgvector para busca semântica
└── CreatedAt / UpdatedAt
```

#### ProductVariant (Variação de Produto)
```
ProductVariant
├── Id: Guid
├── ProductId: Guid
├── SKU: string
├── Attributes: Dictionary<string, string>  ← ex: {"Tamanho": "A2", "Cor": "Branco"}
├── Price: Money
├── StockQuantity: int
└── IsActive: bool
```

#### Category (Categoria)
```
Category
├── Id: Guid
├── Name: string
├── Description: string
├── ParentCategoryId: Guid?  ← hierárquica
├── Slug: string
└── IsActive: bool
```

#### User (Usuário)
```
User
├── Id: Guid (espelha Supabase Auth UID)
├── Email: string
├── Name: string
├── Phone: string?
├── Role: UserRole (Customer | Admin)
├── Addresses: List<Address>
└── CreatedAt
```

#### Address (Endereço)
```
Address
├── Id: Guid
├── UserId: Guid
├── Label: string          ← "Casa", "Trabalho"
├── Street: string
├── Number: string
├── Complement: string?
├── Neighborhood: string
├── City: string
├── State: string (UF)
├── ZipCode: string (CEP)
└── IsDefault: bool
```

#### Order (Pedido)
```
Order
├── Id: Guid
├── UserId: Guid
├── Status: OrderStatus
├── Items: List<OrderItem>
├── ShippingAddress: Address (ValueObject)
├── Subtotal: Money
├── ShippingFee: Money
├── Total: Money
├── PaymentMethod: PaymentMethod (enum)
├── PaymentStatus: PaymentStatus
├── Notes: string?
└── CreatedAt / UpdatedAt
```

#### OrderItem
```
OrderItem
├── Id: Guid
├── OrderId: Guid
├── ProductVariantId: Guid
├── ProductName: string    ← snapshot no momento da compra
├── VariantDescription: string
├── Quantity: int
├── UnitPrice: Money
└── TotalPrice: Money
```

#### Cart (Carrinho — session/user based)
```
Cart
├── Id: Guid
├── UserId: Guid
├── Items: List<CartItem>
├── CreatedAt / UpdatedAt
```

### 4.2 Enums

```
OrderStatus:      Pending | Confirmed | Processing | Shipped | Delivered | Cancelled
PaymentStatus:    Pending | Paid | Refunded | Failed
PaymentMethod:    CreditCard | Pix | Boleto  ← mock no protótipo
UserRole:         Customer | Admin
```

### 4.3 Value Objects
- `Money` — Amount (decimal) + Currency (BRL)
- `Address` (imutável, usado em snapshots de pedido)

---

## 5. CQRS — Commands e Queries

### 5.1 Products
| Type | Name |
|---|---|
| Command | CreateProductCommand |
| Command | UpdateProductCommand |
| Command | DeleteProductCommand |
| Command | AddProductVariantCommand |
| Command | UpdateStockCommand |
| Query | GetProductsQuery (filtros, paginação) |
| Query | GetProductByIdQuery |
| Query | GetProductsByCategoryQuery |

### 5.2 Cart
| Type | Name |
|---|---|
| Command | AddItemToCartCommand |
| Command | UpdateCartItemCommand |
| Command | RemoveItemFromCartCommand |
| Command | ClearCartCommand |
| Query | GetCartQuery |

### 5.3 Orders
| Type | Name |
|---|---|
| Command | CreateOrderCommand |
| Command | UpdateOrderStatusCommand |
| Command | CancelOrderCommand |
| Query | GetOrderByIdQuery |
| Query | GetMyOrdersQuery |
| Query | GetAllOrdersQuery (admin) |

### 5.4 Auth
| Type | Name |
|---|---|
| Command | RegisterUserCommand |
| Command | SyncSupabaseUserCommand |

### 5.5 AI
| Type | Name |
|---|---|
| Query | SemanticSearchQuery |
| Query | GetProductRecommendationsQuery |
| Query | GetAdminInsightsQuery |
| Command | ProcessChatMessageCommand |

---

## 6. Contratos de API (REST)

### Base URL: `/api/v1`

#### Auth
```
POST   /auth/register
POST   /auth/sync           ← sincroniza usuário Supabase com nossa base
```

#### Produtos
```
GET    /products            ?search=&categoryId=&minPrice=&maxPrice=&page=&pageSize=
GET    /products/{id}
POST   /products            [Admin]
PUT    /products/{id}       [Admin]
DELETE /products/{id}       [Admin]
POST   /products/{id}/variants        [Admin]
PUT    /products/{id}/variants/{vid}  [Admin]
```

#### Categorias
```
GET    /categories
GET    /categories/{id}
POST   /categories          [Admin]
PUT    /categories/{id}     [Admin]
```

#### Carrinho
```
GET    /cart
POST   /cart/items
PUT    /cart/items/{itemId}
DELETE /cart/items/{itemId}
DELETE /cart
```

#### Pedidos
```
POST   /orders              ← cria a partir do carrinho
GET    /orders/my           ← pedidos do cliente autenticado
GET    /orders/{id}
GET    /orders              [Admin] ← todos os pedidos
PUT    /orders/{id}/status  [Admin]
```

#### IA
```
POST   /ai/search           body: { query: string }
GET    /ai/recommendations/{productId}
POST   /ai/chat             body: { message: string, sessionId: string }
GET    /ai/insights         [Admin]
```

---

## 7. Arquitetura de IA (dentro do produto)

```
Request do usuário
       │
  ┌────▼───────────────────┐
  │   AI Orchestrator      │  claude-sonnet-4-6
  │   Analisa intenção     │  System prompt: roteador
  └────┬──────┬──────┬─────┘
       │      │      │
  ┌────▼──┐ ┌─▼────┐ ┌▼──────────┐ ┌──────────┐
  │SEARCH │ │RECOM.│ │  SUPPORT  │ │INSIGHTS  │
  │Haiku  │ │Sonnet│ │   Haiku   │ │  Sonnet  │
  └───────┘ └──────┘ └───────────┘ └──────────┘
```

### Agentes e responsabilidades

| Agente | Modelo | Tools disponíveis |
|---|---|---|
| Orchestrator | `claude-sonnet-4-6` | RouteToAgent |
| SearchAgent | `claude-haiku-4-5` | SemanticSearch (pgvector) |
| RecommendationAgent | `claude-sonnet-4-6` | GetProductsByEmbedding, GetOrderHistory |
| SupportAgent | `claude-haiku-4-5` | GetProductInfo, GetOrderStatus |
| InsightsAgent | `claude-sonnet-4-6` | GetSalesData, GetStockData, GetOrderMetrics |

### Fluxo de busca semântica
1. Usuário digita: "kimono azul adulto leve"
2. `SemanticSearchQuery` gera embedding via Anthropic
3. pgvector faz cosine similarity search na tabela `products`
4. Retorna top-10 mais relevantes com score

---

## 8. Autenticação e Autorização

- **Supabase Auth** gerencia registro, login, tokens JWT
- Backend .NET valida JWT via middleware
- Claims extraídas do token: `userId`, `email`, `role`
- Políticas: `[Authorize(Policy = "CustomerOnly")]` / `[Authorize(Policy = "AdminOnly")]`
- Sincronização: ao fazer login pela primeira vez, `SyncSupabaseUserCommand` cria o registro na nossa tabela `users`

---

## 9. Git Flow

```
main          ← código em produção (tags de release)
│
develop       ← integração contínua
│
feature/GOS-{id}-{descricao}   ← novas funcionalidades
bugfix/GOS-{id}-{descricao}    ← correções em develop
release/v{x.y.z}               ← preparação de release
hotfix/GOS-{id}-{descricao}    ← correções urgentes em main
```

**Regras:**
- Nunca commitar direto em `main` ou `develop`
- PR obrigatório com review antes de merge
- Mensagens de commit: `feat(products): add variant management`
- Convenção: Conventional Commits

---

## 10. Skills Claude Code (`.claude/commands/`)

| Skill | Arquivo | Função |
|---|---|---|
| `/spec` | `spec.md` | Escreve/valida specs no padrão SDD |
| `/feature` | `feature.md` | Gera estrutura completa de feature (Domain → API) |
| `/gitflow` | `gitflow.md` | Cria branch no padrão Git Flow correto |
| `/review` | `review.md` | Review de código: Clean Arch, SOLID, CQRS |

---

## 11. MCPs Configurados

| MCP | Propósito |
|---|---|
| Context7 | Docs atualizadas: Next.js, .NET, Supabase, MediatR, EF Core |
| Supabase MCP | Operações diretas no banco durante dev |
| Sequential Thinking | Raciocínio em decisões arquiteturais complexas |

---

## 12. Roadmap do Protótipo (até sexta 02/10)

### Dia 1 — 29/09 (Hoje)
- [ ] Validação deste Spec v0.1
- [ ] Configuração MCPs + Skills
- [ ] Setup repositório + Git Flow
- [ ] Estrutura de projetos (back + front)
- [ ] Schema do banco Supabase
- [ ] Domínio: entidades core

### Dia 2 — 30/09
- [ ] Auth: registro + login (Supabase Auth)
- [ ] CRUD de produtos com variantes (back)
- [ ] CRUD de categorias (back)
- [ ] Listagem de produtos com filtros (front)
- [ ] Detalhe do produto com seleção de variante (front)

### Dia 3 — 01/10
- [ ] Carrinho (back + front)
- [ ] Checkout mock (fluxo completo)
- [ ] Confirmação de pedido
- [ ] Admin: painel de produtos + pedidos
- [ ] IA: busca semântica + recomendações

### Dia 4 — 02/10 (Sexta)
- [ ] IA: chatbot de suporte
- [ ] IA: insights no dashboard admin
- [ ] Polish UI (responsivo, animações)
- [ ] Testes de fluxo completo
- [ ] Preparação da demo

---

## 13. Decisões em Aberto (a decidir em versões futuras)

- Gateway de pagamento: **Mercado Pago** (quando aprovado pelo cliente)
- Auth produção: migrar para ASP.NET Core Identity ou manter Supabase
- Deploy: cloud provider (Azure / Railway / Vercel + Railway)
- Frete: integração com Correios/Melhor Envio
- NFe/Nota fiscal

---

*Spec gerado em 2026-09-29 — GodsOfStruggle v0.1*
