# GodsOfStruggle — Estudo SOLID / Padrões aplicados ao Checkout

> Documento de retomada. Serve para quem continuar (o João, ou uma IA seguindo SDD) saber
> **onde paramos**, **o que já foi decidido** e **qual é o próximo passo**.
> Última atualização: 05/11/2026.

## Contexto

- Branch de estudo: `feat-solid-principles` (a partir de `develop`). Nada disso foi mergeado.
- Objetivo: praticar SOLID e Design Patterns no código real. O **João escreve o código**; a IA orienta e revisa.
- Stack real: .NET 10, Clean Architecture, CQRS (MediatR), FluentValidation, EF Core, Supabase.
- O checkout é uma feature **nova** (não existia nada de `Order` na Application). O Domain já tinha
  `Order`, `OrderItem`, `Money`, `ShippingAddress`, `PaymentMethod`.

## O que já foi praticado

| Conceito | Onde foi aplicado |
|---|---|
| SRP (invariantes na entidade, checagem relacional no handler) | `Category.Create` + `CreateCategoryCommandHandler` |
| Extract Method | `ValidateParentCategoryIdAsync` no handler de categoria |
| Value Object / Primitive Obsession | análise de `Money.cs` |
| Entrada vs. saída (command carrega a entrada; DTO de resposta é a saída) | `CreateOrderCommand`, `OrderItemDto`, `ShippingAddressDto` |
| Não confiar no cliente (preço, status e usuário vêm do servidor) | decisões do `CreateOrderCommand` |
| Validação de forma no validator, regra de negócio no handler | `CreateOrderCommandValidator` |
| `RuleForEach` / `ChildRules` (FluentValidation) | itens e endereço do pedido |
| Contrato de interface x implementação | `ICategoryRepository.ExistsAsync` + `CategoryRepository` |

## Estado atual do código (não commitado)

Pasta `backend/GodsOfStruggle.Application/`:

- `Orders/Commands/CreateOrder/CreateOrderCommand.cs` — **pronto**.
  `List<OrderItemDto> OrderItems, ShippingAddressDto ShippingAddress, PaymentMethod PaymentMethod : IRequest<Guid>`
- `Orders/Commands/CreateOrder/CreateOrderCommandValidator.cs` — **pronto** (itens, endereço, UF `^[A-Z]{2}$`, CEP `^\d{5}-\d{3}$`, enum).
- `Common/DTOs/OrderItemDto.cs` — `ProductVariantId`, `Quantity`.
- `Common/DTOs/ShippingAddressDto.cs` — espelha `ShippingAddress` com `string`.
- `Infrastructure/.../CategoryRepository.cs` — `ExistsAsync` implementado (corrige build quebrado do commit `234cb4a`).

`dotnet build` na pasta `backend` está **verde** (6 projetos).

## Decisões já tomadas

- O command **não** recebe `UserId` (virá de `ICurrentUserService`), nem `OrderStatus` (o pedido nasce `Pending`), nem preço/nome/descrição (o servidor busca pela variante).
- DTOs de entrada ficam em `Common/DTOs` (segue o padrão do projeto). Promover/mover só se surgir motivo.
- Retorno do command: `Guid` do pedido criado (igual ao `AddToCart`).
- O frete **não** vem do cliente. Quando entrar, o command receberá uma modalidade de entrega.

## Próximo passo: `CreateOrderCommandHandler`

Perguntas que o João deve responder **antes** de escrever:

1. O que o handler precisa buscar no banco para montar cada `OrderItem`? (`Order.AddItem` pede nome, descrição, quantidade e `Money` de preço.)
2. Como chegar na variante? `IProductRepository` só tem `GetByIdAsync` de **produto**; decidir entre método novo no repositório ou outra rota.
3. Fluxo: itens vêm do command ou do carrinho do usuário (`ICartRepository`)? Hoje o command recebe itens.
4. Onde checar estoque? Regra de negócio, não de forma: no handler/Domain (`InsufficientStockException` já existe).

Depois do handler: controller fino (`POST`), testes unitários em `Application.Tests` (hoje só o template vazio).

## Roteiro do estudo que vem depois (OCP + Strategy)

1. Checkout funcionando com frete simples (um `if`: grátis acima de um valor, senão taxa fixa) → `Order.SetShippingFee(Money)`.
2. Segunda regra (retirada na loja). Quando o `if/else` incomodar, refatorar para `IShippingStrategy` retornando `Money`.
3. Selecionar a estratégia via DI (`IEnumerable<IShippingStrategy>` ou por chave), **sem recriar o `switch`** em outro lugar.
4. Distinguir Strategy (regra local) de Port/Adapter (`IShippingQuoteProvider` para Correios/Melhor Envio, implementado na Infrastructure).
5. Testar cada estratégia isoladamente.

Só criar a abstração quando houver **pelo menos duas regras reais**.

## Depois do back: frontend

Fazer o fluxo de checkout em Next.js (`app/(store)/checkout`), consumindo o `POST` de pedido.
Pontos de contrato a alinhar com o back: CEP **com hífen**, UF em **maiúsculas**, `PaymentMethod` como enum, quantidade 1–99.

## Cuidados e pendências conhecidas

- **Sempre** buildar a solução inteira (`dotnet build` em `backend/`), não só a Application: o erro do `ExistsAsync` só aparecia na Infrastructure.
- AppLocker bloqueia **executar** a API de `bin\Debug` (ver `PROGRESS.md`); build e testes funcionam.
- `SyncSupabaseUserCommandHandler.cs` tem uma alteração só de espaço em branco, sem relação com o estudo. Descartar ou commitar à parte.
- `AddToCartCommand` aceita `Price` do cliente. É uma fragilidade existente (o preço deveria vir do servidor), fora do escopo do estudo, mas vale uma feature de correção.
- Commits: separar (1) correção do `ExistsAsync`/estudo de categoria, (2) `CreateOrderCommand` + DTOs + validator. Conventional Commits.

## SDD (para retomar com IA)

- O `CLAUDE.md` exige spec antes de feature. A SPEC v0.1 (`.claude/SPEC_v0.1.md`) tem o modelo de `Order`, mas **não** especifica o fluxo de checkout nem o frete.
- Antes de promover este trabalho para uma feature de verdade, rodar `/spec` para o checkout.
- Atenção à numeração: `GOS-005` no git é "Auth Frontend", enquanto os `PROGRESS.md` ainda listam "GOS-005 Checkout". Definir um ID novo para o checkout.
- Rodar a análise de impacto (`/analise-cod`) ao fechar a feature.
