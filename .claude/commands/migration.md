# /migration — Supabase Migration (AppLocker Workaround)

Gera e aplica migrations EF Core no Supabase sem usar `dotnet ef database update` (bloqueado pelo AppLocker).

## Contexto

AppLocker bloqueia `dotnet ef` em pastas do projeto. O fluxo é:
1. Gerar o arquivo `.cs` da migration (se não existir)
2. Extrair o SQL equivalente
3. Rodar o SQL diretamente no Supabase SQL Editor

---

## Fluxo completo

### Passo 1 — Gerar a migration (se AppLocker permitir)

```powershell
# Rodar da pasta raiz da solução
dotnet ef migrations add {NomeDaMigration} \
  --project backend/GodsOfStruggle.Infrastructure \
  --startup-project backend/GodsOfStruggle.API
```

Se AppLocker bloquear, escreva o arquivo `.cs` da migration manualmente baseado no `Up()` que o EF geraria (veja migrations anteriores como modelo).

### Passo 2 — Gerar o SQL da migration

```powershell
dotnet ef migrations script {MigrationAnterior} {NovaMigration} \
  --project backend/GodsOfStruggle.Infrastructure \
  --startup-project backend/GodsOfStruggle.API \
  --output database/migration_{NomeDaMigration}.sql
```

Se AppLocker bloquear o comando acima, extraia o SQL manualmente traduzindo o método `Up()` da migration:

| EF Migration | SQL equivalente |
|---|---|
| `CreateTable(name: "carts", ...)` | `CREATE TABLE "carts" (...)` |
| `Column<Guid>(type: "uuid")` | `uuid NOT NULL` |
| `Column<string>(maxLength: 100)` | `character varying(100) NOT NULL` |
| `Column<decimal>(type: "numeric")` | `numeric NOT NULL` |
| `Column<DateTime>(type: "timestamp with time zone")` | `timestamp with time zone NOT NULL` |
| `Column<bool>` | `boolean NOT NULL` |
| `AddForeignKey(...)` | `ALTER TABLE ... ADD CONSTRAINT ... FOREIGN KEY ...` |
| `CreateIndex(unique: true)` | `CREATE UNIQUE INDEX ... ON ...` |

### Passo 3 — Aplicar no Supabase

1. Abra o Supabase Dashboard → projeto `noahvgfsjtjlcshxkbjt`
2. SQL Editor → New query
3. Cole o SQL gerado
4. Execute

### Passo 4 — Registrar a migration no histórico EF

O EF Core rastreia migrations na tabela `__EFMigrationsHistory`. Adicione manualmente:

```sql
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('{timestamp}_{NomeDaMigration}', '10.0.0');
```

O `MigrationId` deve bater exatamente com o nome do arquivo `.cs` da migration (ex: `20261003120000_AddCart`).

---

## Convenções de coluna deste projeto

- Nomes de tabela: **snake_case** (ex: `cart_items`)
- Nomes de coluna: **PascalCase** via EF (ex: `ProductVariantId`) — **exceto** colunas de Money: `price`, `price_currency`, `base_price`, `price_currency`
- Sempre incluir `CreatedAt` e `UpdatedAt` (herdados de `BaseEntity`)
- PKs são `uuid` geradas pelo cliente (não serial/autoincrement)

---

## Checklist antes de aplicar

- [ ] SQL testado em ambiente de dev antes do Supabase
- [ ] Nenhum `DROP TABLE` ou `ALTER COLUMN` sem backup
- [ ] `__EFMigrationsHistory` atualizado após aplicar
- [ ] Arquivo `.sql` salvo em `database/` para rastreabilidade
