"use client";

import { useEffect, useState, useCallback } from "react";
import { getProducts } from "@/lib/api/products";
import { getCategories } from "@/lib/api/categories";
import { ProductCard } from "@/components/store/ProductCard";
import { CategoryFilter } from "@/components/store/CategoryFilter";
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
      <h1 className="text-3xl font-black text-white mb-2">
        Todos os <span className="text-orange-500">Produtos</span>
      </h1>
      <p className="text-zinc-500 mb-8">Equipamentos premium para atletas de alto nível</p>

      {/* Search */}
      <div className="mb-6">
        <input
          type="text"
          placeholder="Buscar produtos..."
          value={search}
          onChange={(e) => { setSearch(e.target.value); setPage(1); }}
          className="w-full max-w-sm bg-zinc-900 border border-white/10 rounded-lg px-4 py-2.5 text-white placeholder-zinc-500 focus:outline-none focus:border-orange-500 transition-colors"
        />
      </div>

      {/* Category Filter */}
      <div className="mb-8">
        <CategoryFilter categories={categories} selected={categoryId} onChange={handleCategoryChange} />
      </div>

      {loading ? (
        <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
          {Array.from({ length: 8 }).map((_, i) => (
            <div key={i} className="aspect-square bg-zinc-900 rounded-xl animate-pulse" />
          ))}
        </div>
      ) : products?.items.length === 0 ? (
        <div className="text-center py-20 text-zinc-500">
          <p className="text-lg">Nenhum produto encontrado.</p>
        </div>
      ) : (
        <>
          <p className="text-sm text-zinc-500 mb-4">
            {products?.totalCount} produto{products?.totalCount !== 1 ? "s" : ""}
          </p>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
            {products?.items.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>

          {products && products.totalPages > 1 && (
            <div className="flex justify-center gap-2 mt-10">
              <button
                disabled={!products.hasPreviousPage}
                onClick={() => setPage(p => p - 1)}
                className="px-4 py-2 rounded-lg border border-white/10 text-zinc-400 hover:border-orange-500 hover:text-orange-400 disabled:opacity-30 disabled:cursor-not-allowed transition-colors text-sm"
              >
                Anterior
              </button>
              <span className="flex items-center px-4 text-sm text-zinc-500">
                Página {page} de {products.totalPages}
              </span>
              <button
                disabled={!products.hasNextPage}
                onClick={() => setPage(p => p + 1)}
                className="px-4 py-2 rounded-lg border border-white/10 text-zinc-400 hover:border-orange-500 hover:text-orange-400 disabled:opacity-30 disabled:cursor-not-allowed transition-colors text-sm"
              >
                Próxima
              </button>
            </div>
          )}
        </>
      )}
    </div>
  );
}
