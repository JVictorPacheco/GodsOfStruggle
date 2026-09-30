# /review — Code Review (Clean Architecture + SOLID + CQRS)

Revise o código apontado com foco em qualidade arquitetural. Não procure bugs funcionais — isso é tarefa de testes. Foque em design, princípios e manutenibilidade.

## Checklist de revisão

### Clean Architecture
- [ ] Domain não depende de nada externo (sem EF, sem HTTP, sem framework)
- [ ] Application depende só de Domain e interfaces
- [ ] Infrastructure implementa interfaces — não define contratos
- [ ] API é thin — sem lógica de negócio nos controllers
- [ ] Dependências apontam para dentro (Domain ← Application ← Infrastructure ← API)

### SOLID
- [ ] **S** — cada classe tem uma única responsabilidade
- [ ] **O** — aberto para extensão, fechado para modificação (sem switch gigante em negócio)
- [ ] **L** — subtipos substituem o tipo base sem quebrar comportamento
- [ ] **I** — interfaces pequenas e coesas, não "gordas"
- [ ] **D** — depende de abstrações, não de implementações concretas

### CQRS
- [ ] Commands retornam apenas ID ou void — nunca dados completos
- [ ] Queries são somente leitura — sem efeitos colaterais
- [ ] Handlers têm uma única responsabilidade
- [ ] Validators existem para todos os commands com dados de entrada

### Clean Code
- [ ] Nomes revelam intenção — sem abreviações obscuras
- [ ] Funções fazem uma coisa só
- [ ] Sem comentários óbvios ("// get product" antes de GetProduct())
- [ ] Sem código morto (variáveis não usadas, métodos não chamados)
- [ ] Sem magic numbers — use constantes com nome

### Segurança
- [ ] Sem dados sensíveis em logs
- [ ] Inputs validados antes de chegar no domínio
- [ ] Autorização verificada no controller (não no handler)
- [ ] Sem SQL concatenado — somente EF Core ou parameterized queries

## Output do review
Liste cada problema encontrado com:
- **Arquivo:linha** — descrição do problema
- **Regra violada** — qual princípio
- **Sugestão** — como corrigir

Se não houver problemas: "APROVADO — nenhuma violação encontrada."
