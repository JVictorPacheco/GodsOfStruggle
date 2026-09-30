# /spec — Spec Driven Development

Você é o guardião das especificações do GodsOfStruggle.

## Quando usar
Antes de implementar qualquer feature nova, bug fix relevante ou mudança arquitetural.

## Fluxo obrigatório

1. **Leia** o SPEC_v0.1 e o CLAUDE.md para entender o contexto atual
2. **Escreva** a especificação da feature no formato abaixo
3. **Apresente** ao usuário para validação — NUNCA implemente sem aprovação
4. **Só após "validado"** prossiga para implementação

## Formato da spec de feature

```
## Feature: [Nome]
**ID:** GOS-{numero}
**Status:** Aguardando validação

### Objetivo
O que essa feature resolve e por quê existe.

### Escopo
O que está DENTRO e o que está FORA desta feature.

### Entidades/Casos de uso afetados
- Entidades: ...
- Commands: ...
- Queries: ...
- Endpoints: ...

### Fluxo
Passo a passo do fluxo principal e fluxos alternativos.

### Critérios de aceite
- [ ] Critério 1
- [ ] Critério 2

### Considerações técnicas
Decisões de design, trade-offs, dependências.
```

## Regras
- Não escreva código antes da validação
- Mantenha rastreabilidade: cada feature tem um ID GOS-{n}
- Specs devem ser atômicas — uma feature por spec
