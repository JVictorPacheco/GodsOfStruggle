"use client";

import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import Image from "next/image";
import { getProductById } from "@/lib/api/products";
import type { Product, ProductVariant } from "@/types";

function formatPrice(amount: number, currency: string) {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency }).format(amount);
}

export default function ProductDetailPage() {
  const { id } = useParams<{ id: string }>();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);
  const [selectedVariant, setSelectedVariant] = useState<ProductVariant | null>(null);
  const [selectedImage, setSelectedImage] = useState(0);

  useEffect(() => {
    getProductById(id)
      .then((p) => { setProduct(p); setSelectedVariant(p.variants.find(v => v.isActive) ?? null); })
      .catch(() => {})
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return (
    <div className="max-w-7xl mx-auto px-4 py-10">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-10">
        <div className="aspect-square bg-zinc-900 rounded-xl animate-pulse" />
        <div className="space-y-4">
          <div className="h-8 bg-zinc-900 rounded animate-pulse w-3/4" />
          <div className="h-6 bg-zinc-900 rounded animate-pulse w-1/4" />
          <div className="h-24 bg-zinc-900 rounded animate-pulse" />
        </div>
      </div>
    </div>
  );

  if (!product) return (
    <div className="max-w-7xl mx-auto px-4 py-20 text-center text-zinc-500">
      Produto não encontrado.
    </div>
  );

  const attributeKeys = [...new Set(product.variants.flatMap(v => Object.keys(v.attributes)))];

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-10">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-10">

        {/* Image Gallery */}
        <div className="space-y-3">
          <div className="relative aspect-square rounded-xl overflow-hidden bg-zinc-900 border border-white/10">
            {product.images[selectedImage] ? (
              <Image
                src={product.images[selectedImage].url}
                alt={product.images[selectedImage].altText}
                fill
                className="object-cover"
              />
            ) : (
              <div className="w-full h-full flex items-center justify-center text-zinc-700">
                <svg className="w-24 h-24" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
                </svg>
              </div>
            )}
          </div>
          {product.images.length > 1 && (
            <div className="flex gap-2">
              {product.images.map((img, i) => (
                <button key={img.id} onClick={() => setSelectedImage(i)}
                  className={`relative w-16 h-16 rounded-lg overflow-hidden border-2 transition-colors ${i === selectedImage ? "border-orange-500" : "border-white/10 hover:border-white/30"}`}>
                  <Image src={img.url} alt={img.altText} fill className="object-cover" />
                </button>
              ))}
            </div>
          )}
        </div>

        {/* Product Info */}
        <div className="space-y-6">
          {product.categoryName && (
            <span className="text-xs font-semibold text-orange-500 uppercase tracking-widest">
              {product.categoryName}
            </span>
          )}
          <h1 className="text-3xl font-black text-white">{product.name}</h1>
          <p className="text-3xl font-bold text-orange-400">
            {formatPrice(selectedVariant?.price ?? product.basePrice, product.currency)}
          </p>

          <div className="h-px bg-white/10" />

          {/* Variant Selectors */}
          {attributeKeys.map((key) => {
            const values = [...new Set(product.variants.filter(v => v.isActive).map(v => v.attributes[key]).filter(Boolean))];
            return (
              <div key={key}>
                <p className="text-sm font-semibold text-zinc-300 mb-3 uppercase tracking-wide">{key}</p>
                <div className="flex flex-wrap gap-2">
                  {values.map((val) => {
                    const isSelected = selectedVariant?.attributes[key] === val;
                    return (
                      <button key={val}
                        onClick={() => {
                          const match = product.variants.find(v => v.isActive && v.attributes[key] === val);
                          if (match) setSelectedVariant(match);
                        }}
                        className={`px-4 py-2 rounded-lg border text-sm font-medium transition-all ${
                          isSelected
                            ? "bg-orange-500 text-black border-orange-500"
                            : "border-white/10 text-zinc-300 hover:border-orange-500/50 hover:text-white bg-zinc-900"
                        }`}>
                        {val}
                      </button>
                    );
                  })}
                </div>
              </div>
            );
          })}

          <div className="h-px bg-white/10" />

          <p className="text-zinc-400 leading-relaxed">{product.description}</p>

          {selectedVariant && (
            <p className="text-sm">
              {selectedVariant.stockQuantity > 0
                ? <span className="text-blue-400">{selectedVariant.stockQuantity} unidades em estoque</span>
                : <span className="text-red-500">Fora de estoque</span>}
            </p>
          )}

          <button
            disabled={!selectedVariant || selectedVariant.stockQuantity === 0}
            className="w-full py-4 rounded-xl font-bold text-sm uppercase tracking-wide transition-all
              bg-orange-500 hover:bg-orange-400 text-black
              disabled:bg-zinc-800 disabled:text-zinc-600 disabled:cursor-not-allowed"
          >
            Adicionar ao Carrinho
          </button>
        </div>
      </div>
    </div>
  );
}
