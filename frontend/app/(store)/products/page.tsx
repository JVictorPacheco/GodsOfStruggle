"use client";

import { useEffect, useState, useCallback } from "react";
import { getProducts } from "@/lib/api/products";
import { getCategories } from "@/lib/api/categories";
import { ProductCard } from "@/components/store/ProductCard";
import { CategoryFilter } from "@/components/store/CategoryFilter";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import type { Category, PagedResult, ProductListItem } from "@/types";

export default function ProductsPage() {
  const [products, setProducts] = useState<PagedResult<ProductListItem> | null>(null);
  const [categories, setCategories] = useState<Category[]>([]);
  const [search, setSearch] = useState("");
  const [categoryId, setCategoryId] = useState<string | null>(null);
  const [page, setPage] = useState(1);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getCategories().then(setCategories).catch(() => {});
  }, []);

  const fetchProducts = useCallback(async () => {
    setLoading(true);
    try {
      const result = await getProducts({ search: search || undefined, categoryId: categoryId ?? undefined, page });
      setProducts(result);
    } finally {
      setLoading(false);
    }
  }, [search, categoryId, page]);

  useEffect(() => { fetchProducts(); }, [fetchProducts]);

  const handleCategoryChange = (id: string | null) => { setCategoryId(id); setPage(1); };

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-10">
      <h1 className="text-3xl font-bold mb-8">Produtos</h1>

      <div className="flex flex-col sm:flex-row gap-4 mb-6">
        <Input
          placeholder="Buscar produtos..."
          value={search}
          onChange={(e) => { setSearch(e.target.value); setPage(1); }}
          className="max-w-sm"
        />
      </div>

      <div className="mb-6">
        <CategoryFilter categories={categories} selected={categoryId} onChange={handleCategoryChange} />
      </div>

      {loading ? (
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          {Array.from({ length: 8 }).map((_, i) => (
            <div key={i} className="aspect-square bg-zinc-100 rounded-lg animate-pulse" />
          ))}
        </div>
      ) : products?.items.length === 0 ? (
        <div className="text-center py-20 text-zinc-400">
          <p className="text-lg">Nenhum produto encontrado.</p>
        </div>
      ) : (
        <>
          <p className="text-sm text-zinc-500 mb-4">{products?.totalCount} produto{products?.totalCount !== 1 ? "s" : ""}</p>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
            {products?.items.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>

          {products && products.totalPages > 1 && (
            <div className="flex justify-center gap-2 mt-10">
              <Button variant="outline" disabled={!products.hasPreviousPage} onClick={() => setPage(p => p - 1)}>Anterior</Button>
              <span className="flex items-center px-4 text-sm text-zinc-500">Página {page} de {products.totalPages}</span>
              <Button variant="outline" disabled={!products.hasNextPage} onClick={() => setPage(p => p + 1)}>Próxima</Button>
            </div>
          )}
        </>
      )}
    </div>
  );
}
