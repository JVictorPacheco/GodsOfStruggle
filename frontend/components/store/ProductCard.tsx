import Link from "next/link";
import Image from "next/image";
import type { ProductListItem } from "@/types";

function formatPrice(amount: number, currency: string) {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency }).format(amount);
}

export function ProductCard({ product }: { product: ProductListItem }) {
  return (
    <Link href={`/products/${product.id}`} className="group block">
      <div className="overflow-hidden rounded-xl border border-white/10 bg-zinc-900 hover:border-orange-500/40 transition-all duration-200 hover:shadow-[0_0_20px_rgba(249,115,22,0.1)]">
        <div className="relative aspect-square bg-zinc-800 overflow-hidden">
          {product.primaryImageUrl ? (
            <Image
              src={product.primaryImageUrl}
              alt={product.name}
              fill
              sizes="(max-width: 768px) 50vw, 25vw"
              className="object-cover group-hover:scale-105 transition-transform duration-300"
            />
          ) : (
            <div className="w-full h-full flex items-center justify-center text-zinc-600">
              <svg className="w-16 h-16" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
              </svg>
            </div>
          )}
        </div>
        <div className="p-4">
          {product.categoryName && (
            <span className="text-xs font-semibold text-orange-500 uppercase tracking-wide">
              {product.categoryName}
            </span>
          )}
          <h3 className="font-semibold text-sm leading-tight line-clamp-2 mt-1 mb-1 text-white">
            {product.name}
          </h3>
          <p className="text-xs text-zinc-500 mb-2">
            {product.variantCount} variação{product.variantCount !== 1 ? "ões" : ""}
          </p>
          <p className="font-bold text-base text-orange-400">
            {formatPrice(product.basePrice, product.currency)}
          </p>
        </div>
      </div>
    </Link>
  );
}
