# GodsOfStruggle — Progress Tracker

## Status Atual
**Fase:** GOS-005 em andamento — Auth Frontend (branch: feature/GOS-005-auth-frontend)
**Branch ativa:** `feature/GOS-005-auth-frontend`
**Última atualização:** 2026-10-04

---

## Concluído

### Setup (Dia 1 — 29-30/09)
- [x] Spec v0.1 validado (ver `.claude/SPEC_v0.1.md`)
- [x] Repositório GitHub: https://github.com/JVictorPacheco/GodsOfStruggle (privado)
- [x] Git Flow: `main` + `develop` configurados
- [x] Solução .NET 10 com 6 projetos (Clean Architecture)
- [x] Referências entre camadas configuradas
- [x] Pacotes NuGet instalados (MediatR, FluentValidation, AutoMapper, EF Core, Npgsql, pgvector, Anthropic.SDK, JWT Bearer, Swagger)
- [x] Next.js 16 + TypeScript + Tailwind CSS + App Router
- [x] shadcn/ui inicializado com componentes base
- [x] Dependências frontend (Supabase, Anthropic SDK, Zustand, Zod, React Hook Form)
- [x] Estrutura interna de pastas (back + front)
- [x] Skills: `/spec` `/feature` `/gitflow` `/review`
- [x] CLAUDE.md do projeto
- [x] MCPs: Context7 + Supabase configurados em `.mcp.json`

---

## Próximo — Dia 2 (30/09)

### Bloqueio pendente
- [ ] **Supabase**: criar projeto e obter credenciais (URL, Anon Key, Service Role Key)

### Após credenciais Supabase
- [ ] `feature/GOS-001-domain-entities` — Entidades de domínio (Product, ProductVariant, Category, User, Order, Cart)
- [ ] Schema SQL do Supabase (migrations)
- [ ] Configurar EF Core + DbContext
- [ ] `feature/GOS-002-auth` — Supabase Auth + JWT no .NET

### Dia 3 (01/10)
- [ ] `feature/GOS-003-products` — CRUD produtos com variantes (back + front paralelo)
- [ ] `feature/GOS-004-cart` — Carrinho
- [ ] `feature/GOS-005-checkout` — Checkout mock

### Dia 4 — Sexta (02/10)
- [ ] `feature/GOS-006-ai` — Busca semântica + recomendações + chatbot
- [ ] `feature/GOS-007-admin` — Dashboard admin com insights IA
- [ ] Polish UI + demo prep

---

## Decisões tomadas
- Banco: Supabase (PostgreSQL + pgvector)
- Auth: Supabase Auth para o protótipo
- Payment: Mock checkout (sem gateway real no protótipo)
- Chatbot: escopo produto/pedido apenas
- Modelos IA: Sonnet (orchestrator/insights) + Haiku (search/chat)
- Produto: atributos dinâmicos desde o início (genérico)
