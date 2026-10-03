-- GodsOfStruggle — Seed Data
-- Run this in Supabase SQL Editor

-- Categories
INSERT INTO categories (id, name, slug, description, is_active, created_at, updated_at) VALUES
  ('a1b2c3d4-0001-0000-0000-000000000001', 'Kimonos BJJ', 'kimonos-bjj', 'Kimonos para Brazilian Jiu-Jitsu', true, now(), now()),
  ('a1b2c3d4-0001-0000-0000-000000000002', 'Kimonos Judô', 'kimonos-judo', 'Kimonos para Judô', true, now(), now()),
  ('a1b2c3d4-0001-0000-0000-000000000003', 'Rashguards', 'rashguards', 'Rashguards e lycras para treino', true, now(), now()),
  ('a1b2c3d4-0001-0000-0000-000000000004', 'Acessórios', 'acessorios', 'Faixas, bolsas e acessórios', true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Products (sem slug/sku — não existem na tabela)
INSERT INTO products (id, name, description, base_price, price_currency, is_active, category_id, created_at, updated_at) VALUES
  (
    'b2c3d4e5-0002-0000-0000-000000000001',
    'Kimono BJJ Gods Pro — Branco',
    'Kimono de alto desempenho para competição e treino. Tecido 100% algodão pré-encolhido, costuras reforçadas com ponto duplo. Aprovado pela IBJJF.',
    399.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000001', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000002',
    'Kimono BJJ Gods Pro — Azul',
    'Kimono de alto desempenho para competição e treino. Tecido 100% algodão pré-encolhido, costuras reforçadas com ponto duplo. Aprovado pela IBJJF.',
    399.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000001', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000003',
    'Kimono BJJ Gods Elite — Preto',
    'Linha Elite para atletas de alto nível. Tecido pearl weave ultra-leve, gola em couro sintético reforçado. Bordado exclusivo Gods of Struggle.',
    549.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000001', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000004',
    'Kimono Judô Gods Kime',
    'Kimono de Judô certificado pela IJF. Tecido double weave resistente a torções e quedas. Ideal de faixas branca a preta.',
    479.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000002', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000005',
    'Rashguard Manga Longa Gods Strike',
    'Rashguard de compressão para BJJ, MMA e academias. Tecido UPF 50+, costuras flatlock anti-abrasão. Secagem ultra-rápida.',
    199.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000003', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000006',
    'Rashguard Manga Curta Gods Slash',
    'Rashguard leve para treino de verão. Tecido 4-way stretch, estampa sublimada exclusiva. Compatível com shorts de BJJ e MMA.',
    169.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000003', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000007',
    'Faixa BJJ Gods — Algodão Premium',
    'Faixa oficial Gods of Struggle em algodão 100% penteado. Barra reforçada, tamanhos A0 ao A5. Disponível em todas as cores.',
    59.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000004', now(), now()
  ),
  (
    'b2c3d4e5-0002-0000-0000-000000000008',
    'Bolsa Gods Gear Bag',
    'Bolsa esportiva com compartimento para kimono úmido separado. 60L de capacidade, alça de mochila + alça de mão. Material impermeável.',
    279.90, 'BRL', true, 'a1b2c3d4-0001-0000-0000-000000000004', now(), now()
  )
ON CONFLICT (id) DO NOTHING;

-- Product Variants (coluna correta: "price", não "price_amount")

-- Kimono BJJ Pro Branco (A1–A5)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0001-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000001', 'GOS-BJJ-PRO-WHT-A1', '{"Tamanho":"A1"}', 399.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0001-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000001', 'GOS-BJJ-PRO-WHT-A2', '{"Tamanho":"A2"}', 399.90, 'BRL', 15, true, now(), now()),
  ('c3d4e5f6-0003-0001-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000001', 'GOS-BJJ-PRO-WHT-A3', '{"Tamanho":"A3"}', 399.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0001-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000001', 'GOS-BJJ-PRO-WHT-A4', '{"Tamanho":"A4"}', 399.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0001-0001-000000000005', 'b2c3d4e5-0002-0000-0000-000000000001', 'GOS-BJJ-PRO-WHT-A5', '{"Tamanho":"A5"}', 399.90, 'BRL', 5, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Kimono BJJ Pro Azul (A1–A5)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0002-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000002', 'GOS-BJJ-PRO-BLU-A1', '{"Tamanho":"A1"}', 399.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0002-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000002', 'GOS-BJJ-PRO-BLU-A2', '{"Tamanho":"A2"}', 399.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0002-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000002', 'GOS-BJJ-PRO-BLU-A3', '{"Tamanho":"A3"}', 399.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0002-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000002', 'GOS-BJJ-PRO-BLU-A4', '{"Tamanho":"A4"}', 399.90, 'BRL', 6, true, now(), now()),
  ('c3d4e5f6-0003-0002-0001-000000000005', 'b2c3d4e5-0002-0000-0000-000000000002', 'GOS-BJJ-PRO-BLU-A5', '{"Tamanho":"A5"}', 399.90, 'BRL', 4, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Kimono BJJ Elite Preto (A1–A5)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0003-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000003', 'GOS-BJJ-ELT-BLK-A1', '{"Tamanho":"A1"}', 549.90, 'BRL', 5, true, now(), now()),
  ('c3d4e5f6-0003-0003-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000003', 'GOS-BJJ-ELT-BLK-A2', '{"Tamanho":"A2"}', 549.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0003-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000003', 'GOS-BJJ-ELT-BLK-A3', '{"Tamanho":"A3"}', 549.90, 'BRL', 7, true, now(), now()),
  ('c3d4e5f6-0003-0003-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000003', 'GOS-BJJ-ELT-BLK-A4', '{"Tamanho":"A4"}', 549.90, 'BRL', 4, true, now(), now()),
  ('c3d4e5f6-0003-0003-0001-000000000005', 'b2c3d4e5-0002-0000-0000-000000000003', 'GOS-BJJ-ELT-BLK-A5', '{"Tamanho":"A5"}', 549.90, 'BRL', 2, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Kimono Judô (P–GG)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0004-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000004', 'GOS-JUD-KIME-P', '{"Tamanho":"P"}', 479.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0004-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000004', 'GOS-JUD-KIME-M', '{"Tamanho":"M"}', 479.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0004-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000004', 'GOS-JUD-KIME-G', '{"Tamanho":"G"}', 479.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0004-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000004', 'GOS-JUD-KIME-GG', '{"Tamanho":"GG"}', 479.90, 'BRL', 6, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Rashguard Manga Longa (P–GG, Preto e Branco)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0005-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-P-BLK',  '{"Tamanho":"P","Cor":"Preto"}',   199.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-M-BLK',  '{"Tamanho":"M","Cor":"Preto"}',   199.90, 'BRL', 15, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-G-BLK',  '{"Tamanho":"G","Cor":"Preto"}',   199.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-GG-BLK', '{"Tamanho":"GG","Cor":"Preto"}',  199.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000005', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-P-WHT',  '{"Tamanho":"P","Cor":"Branco"}',  199.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000006', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-M-WHT',  '{"Tamanho":"M","Cor":"Branco"}',  199.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000007', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-G-WHT',  '{"Tamanho":"G","Cor":"Branco"}',  199.90, 'BRL', 9, true, now(), now()),
  ('c3d4e5f6-0003-0005-0001-000000000008', 'b2c3d4e5-0002-0000-0000-000000000005', 'GOS-RG-STRIKE-LS-GG-WHT', '{"Tamanho":"GG","Cor":"Branco"}', 199.90, 'BRL', 5, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Rashguard Manga Curta (P–GG)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0006-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000006', 'GOS-RG-SLASH-SS-P',  '{"Tamanho":"P"}',  169.90, 'BRL', 10, true, now(), now()),
  ('c3d4e5f6-0003-0006-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000006', 'GOS-RG-SLASH-SS-M',  '{"Tamanho":"M"}',  169.90, 'BRL', 14, true, now(), now()),
  ('c3d4e5f6-0003-0006-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000006', 'GOS-RG-SLASH-SS-G',  '{"Tamanho":"G"}',  169.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0006-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000006', 'GOS-RG-SLASH-SS-GG', '{"Tamanho":"GG"}', 169.90, 'BRL', 7, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Faixas BJJ (todas as cores)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0007-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000007', 'GOS-BELT-WHT', '{"Cor":"Branca","Tamanho":"A0"}',  59.90, 'BRL', 20, true, now(), now()),
  ('c3d4e5f6-0003-0007-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000007', 'GOS-BELT-BLU', '{"Cor":"Azul","Tamanho":"A2"}',    59.90, 'BRL', 15, true, now(), now()),
  ('c3d4e5f6-0003-0007-0001-000000000003', 'b2c3d4e5-0002-0000-0000-000000000007', 'GOS-BELT-PUR', '{"Cor":"Roxa","Tamanho":"A2"}',    59.90, 'BRL', 12, true, now(), now()),
  ('c3d4e5f6-0003-0007-0001-000000000004', 'b2c3d4e5-0002-0000-0000-000000000007', 'GOS-BELT-BRW', '{"Cor":"Marrom","Tamanho":"A2"}',  59.90, 'BRL', 8, true, now(), now()),
  ('c3d4e5f6-0003-0007-0001-000000000005', 'b2c3d4e5-0002-0000-0000-000000000007', 'GOS-BELT-BLK', '{"Cor":"Preta","Tamanho":"A2"}',   79.90, 'BRL', 5, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Bolsa Gear Bag (duas cores)
INSERT INTO product_variants (id, product_id, sku, attributes, price, price_currency, stock_quantity, is_active, created_at, updated_at) VALUES
  ('c3d4e5f6-0003-0008-0001-000000000001', 'b2c3d4e5-0002-0000-0000-000000000008', 'GOS-BAG-GEAR-BLK', '{"Cor":"Preto"}', 279.90, 'BRL', 20, true, now(), now()),
  ('c3d4e5f6-0003-0008-0001-000000000002', 'b2c3d4e5-0002-0000-0000-000000000008', 'GOS-BAG-GEAR-KHK', '{"Cor":"Cáqui"}', 279.90, 'BRL', 10, true, now(), now())
ON CONFLICT (id) DO NOTHING;

-- Product Images
INSERT INTO product_images (id, product_id, url, alt_text, display_order, is_primary, created_at, updated_at) VALUES
  ('d4e5f6a7-0004-0001-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000001', 'https://images.unsplash.com/photo-1544717297-fa95b6ee9643?w=600&q=80', 'Kimono BJJ Pro Branco', 0, true, now(), now()),
  ('d4e5f6a7-0004-0002-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000002', 'https://images.unsplash.com/photo-1555597673-b21d5c935865?w=600&q=80', 'Kimono BJJ Pro Azul', 0, true, now(), now()),
  ('d4e5f6a7-0004-0003-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000003', 'https://images.unsplash.com/photo-1571019613454-1cb2f99b2d8b?w=600&q=80', 'Kimono BJJ Elite Preto', 0, true, now(), now()),
  ('d4e5f6a7-0004-0004-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000004', 'https://images.unsplash.com/photo-1626379961798-54f819ee896a?w=600&q=80', 'Kimono Judô Kime', 0, true, now(), now()),
  ('d4e5f6a7-0004-0005-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000005', 'https://images.unsplash.com/photo-1517963879433-6ad2b056d712?w=600&q=80', 'Rashguard Manga Longa Strike', 0, true, now(), now()),
  ('d4e5f6a7-0004-0006-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000006', 'https://images.unsplash.com/photo-1603217040830-8be0c3fe0547?w=600&q=80', 'Rashguard Manga Curta Slash', 0, true, now(), now()),
  ('d4e5f6a7-0004-0007-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000007', 'https://images.unsplash.com/photo-1576240299893-7db1ba0bf584?w=600&q=80', 'Faixa BJJ Gods', 0, true, now(), now()),
  ('d4e5f6a7-0004-0008-0000-000000000001', 'b2c3d4e5-0002-0000-0000-000000000008', 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?w=600&q=80', 'Bolsa Gods Gear Bag', 0, true, now(), now())
ON CONFLICT (id) DO NOTHING;
