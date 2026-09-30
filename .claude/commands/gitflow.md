# /gitflow — Git Flow Helper

Gerencia branches e commits seguindo o Git Flow do GodsOfStruggle.

## Estrutura de branches

```
main          ← produção (tags de release)
develop       ← integração
feature/GOS-{id}-{descricao-kebab}
bugfix/GOS-{id}-{descricao-kebab}
release/v{major}.{minor}.{patch}
hotfix/GOS-{id}-{descricao-kebab}
```

## Conventional Commits obrigatório

```
feat(scope): mensagem
fix(scope): mensagem
chore(scope): mensagem
refactor(scope): mensagem
test(scope): mensagem
docs(scope): mensagem
```

Scopes válidos: `products`, `orders`, `cart`, `auth`, `ai`, `admin`, `infra`, `domain`

## Quando usar cada tipo de branch

| Situação | Branch | Base | Merge para |
|---|---|---|---|
| Nova feature | feature/GOS-{n}-{desc} | develop | develop |
| Bug em develop | bugfix/GOS-{n}-{desc} | develop | develop |
| Preparar release | release/v{x.y.z} | develop | main + develop |
| Bug em produção | hotfix/GOS-{n}-{desc} | main | main + develop |

## Fluxo ao implementar uma feature

1. Criar branch: `git checkout -b feature/GOS-{n}-{desc} develop`
2. Implementar em commits atômicos com Conventional Commits
3. Push: `git push origin feature/GOS-{n}-{desc}`
4. PR para develop com título: `feat(scope): descrição`
5. Após merge: deletar branch local e remota

## Regras
- NUNCA commitar direto em `main` ou `develop`
- Um commit por responsabilidade lógica — não commitar "tudo de uma vez"
- Mensagem de commit no imperativo: "add product entity" não "added product entity"
