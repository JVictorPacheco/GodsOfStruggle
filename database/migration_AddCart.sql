-- GOS-004: Add Cart and CartItems tables
-- Apply in Supabase SQL Editor

CREATE TABLE IF NOT EXISTS "carts" (
    "Id"        uuid        NOT NULL,
    "UserId"    uuid        NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_carts" PRIMARY KEY ("Id"),
    CONSTRAINT "UQ_carts_UserId" UNIQUE ("UserId"),
    CONSTRAINT "FK_carts_users_UserId" FOREIGN KEY ("UserId") REFERENCES "users" ("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "cart_items" (
    "Id"                 uuid                     NOT NULL,
    "CartId"             uuid                     NOT NULL,
    "ProductVariantId"   uuid                     NOT NULL,
    "ProductName"        character varying(200)   NOT NULL,
    "VariantDescription" character varying(200)   NOT NULL DEFAULT '',
    "Quantity"           integer                  NOT NULL,
    "price"              numeric(18,2)             NOT NULL,
    "price_currency"     character varying(3)     NOT NULL DEFAULT 'BRL',
    "CreatedAt"          timestamp with time zone NOT NULL,
    "UpdatedAt"          timestamp with time zone NOT NULL,
    CONSTRAINT "PK_cart_items" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_cart_items_carts_CartId" FOREIGN KEY ("CartId") REFERENCES "carts" ("Id") ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS "IX_cart_items_CartId" ON "cart_items" ("CartId");

-- Register migration in EF history
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20261003000000_AddCart', '10.0.0')
ON CONFLICT DO NOTHING;
