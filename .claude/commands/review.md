# /review — Code Review (Clean Architecture + SOLID + CQRS + Security)

Revise o código apontado com foco em qualidade arquitetural e segurança. Execute antes de qualquer PR.

## Checklist de revisão

### Clean Architecture
- [ ] Domain não depende de nada externo (sem EF, sem HTTP, sem framework)
- [ ] Application depende só de Domain e interfaces
- [ ] Infrastructure implementa interfaces — não define contratos
- [ ] API é thin — sem lógica de negócio nos controllers
- [ ] Dependências apontam para dentro (Domain ← Application ← Infrastructure ← API)

### SOLID
- [ ] **S** — cada classe tem uma única responsabilidade
- [ ] **O** — aberto para extensão, fechado para modificação
- [ ] **L** — subtipos substituem o tipo base sem quebrar comportamento
- [ ] **I** — interfaces pequenas e coesas
- [ ] **D** — depende de abstrações, não de implementações concretas

### CQRS
- [ ] Commands retornam apenas ID ou void — nunca dados completos
- [ ] Queries são somente leitura — sem efeitos colaterais
- [ ] Handlers têm uma única responsabilidade
- [ ] Validators existem para todos os commands com dados de entrada

### Clean Code
- [ ] Nomes revelam intenção — sem abreviações obscuras
- [ ] Funções fazem uma coisa só
- [ ] Sem comentários óbvios
- [ ] Sem código morto
- [ ] Sem magic numbers

### Segurança — Autorização
- [ ] `[Authorize]` presente em todos os endpoints que modificam dados
- [ ] Endpoints Admin verificam `ICurrentUserService.IsAdmin` explicitamente
- [ ] Recursos user-scoped (Cart, Orders): handler usa `ICurrentUserService.UserId`, não parâmetro externo
- [ ] Usuário autenticado não consegue acessar recursos de outro usuário (anti-IDOR)
- [ ] 401 para não autenticado, 403 para não autorizado, 404 para não encontrado — nunca 200 com dados errados

### Segurança — Input e Output
- [ ] Inputs validados com FluentValidation antes de chegar no domínio
- [ ] Quantidades numéricas têm limites (`> 0`, `<= 99` para itens de carrinho)
- [ ] Sem SQL raw concatenado — somente EF Core ou parâmetros nomeados
- [ ] Respostas de erro não expõem stack traces ou detalhes internos
- [ ] Nenhuma propriedade sensível (stock de backoffice, cost price) em DTOs públicos

## Output do review
Liste cada problema encontrado com:
- **Arquivo:linha** — descrição do problema
- **Regra violada** — qual princípio (Clean Arch / SOLID / CQRS / Security-IDOR / Security-AuthZ / Security-Input)
- **Sugestão** — como corrigir

Se não houver problemas: "APROVADO — nenhuma violação encontrada."
