# GodsOfStruggle — CLAUDE.md

## Visão do Projeto
Plataforma de e-commerce para kimonos e artigos de luta (arquitetada para qualquer produto).  
Protótipo para apresentação ao cliente — prazo: 02/10/2026.

## Stack
- **Frontend:** Next.js 16 + TypeScript + Tailwind CSS + shadcn/ui
- **Backend:** .NET 10 + MediatR (CQRS) + EF Core + Npgsql
- **Banco:** Supabase (PostgreSQL + pgvector)
- **Auth:** Supabase Auth + JWT (validado no .NET)
- **IA:** Anthropic SDK — claude-sonnet-4-6 (orchestrator/insights/recomendações) + claude-haiku-4-5 (search/chatbot)

## Estrutura do Repositório
```
GodsOfStruggle/
├── backend/                 ← Solução .NET (Clean Architecture)
│   ├── GodsOfStruggle.Domain/
│   ├── GodsOfStruggle.Application/
│   ├── GodsOfStruggle.Infrastructure/
│   ├── GodsOfStruggle.API/
│   ├── GodsOfStruggle.Domain.Tests/
│   └── GodsOfStruggle.Application.Tests/
├── frontend/                ← Next.js App Router
│   ├── app/
│   │   ├── (store)/         ← Rotas públicas da loja
│   │   └── (admin)/         ← Painel administrativo
│   ├── components/
│   │   ├── ui/              ← shadcn/ui
│   │   ├── store/
│   │   └── admin/
│   ├── lib/
│   │   ├── api/
│   │   └── ai/
│   └── types/
└── .claude/commands/        ← Skills: /spec /feature /gitflow /review
```

## Metodologia
**Spec Driven Development** — nenhum código sem spec validada.  
Use `/spec` antes de qualquer feature nova.

## Arquitetura — Regras Invioláveis

### Dependências (Clean Architecture)
```
Domain ← Application ← Infrastructure ← API
```
- **Domain**: zero dependências externas. Entidades ricas.
- **Application**: só interfaces. Handlers via MediatR.
- **Infrastructure**: implementa interfaces. EF Core aqui.
- **API**: controllers finos. Só MediatR + retorno HTTP.

### CQRS
- Commands: escrita, sem retorno de dados completos
- Queries: leitura pura, sem efeitos colaterais
- Todo command com input tem FluentValidation

### Código
- Sem comentários óbvios
- Sem `var` quando tipo não é evidente
- Result pattern para erros de domínio
- Nomes em inglês no código, mensagens para usuário em PT-BR

## Git Flow
```
main ← develop ← feature/GOS-{id}-{descricao}
```
Conventional Commits: `feat(products): add variant management`  
Use `/gitflow` para criar branches.

## Skills disponíveis
- `/spec` — escrever e validar specs antes de implementar
- `/feature` — gerar estrutura de feature seguindo Clean Arch + CQRS
- `/gitflow` — criar branch no padrão correto
- `/review` — revisar código contra princípios do projeto

## Agentes de IA no Produto
| Agente | Modelo | Função |
|---|---|---|
| Orchestrator | claude-sonnet-4-6 | Roteia requests de IA |
| SearchAgent | claude-haiku-4-5 | Busca semântica (pgvector) |
| RecommendationAgent | claude-sonnet-4-6 | Produtos relacionados |
| SupportAgent | claude-haiku-4-5 | Chatbot produto/pedido |
| InsightsAgent | claude-sonnet-4-6 | Dashboard admin |

## Variáveis de Ambiente necessárias

### Backend (appsettings.json / secrets)
```
Supabase:Url
Supabase:AnonKey
Supabase:ServiceRoleKey
Anthropic:ApiKey
Jwt:Secret
Jwt:Issuer
Jwt:Audience
ConnectionStrings:DefaultConnection
```

### Frontend (.env.local)
```
NEXT_PUBLIC_SUPABASE_URL
NEXT_PUBLIC_SUPABASE_ANON_KEY
NEXT_PUBLIC_API_URL
ANTHROPIC_API_KEY
```
