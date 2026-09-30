import Link from "next/link";
import Image from "next/image";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent } from "@/components/ui/card";
import type { ProductListItem } from "@/types";

function formatPrice(amount: number, currency: string) {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency }).format(amount);
}

export function ProductCard({ product }: { product: ProductListItem }) {
  return (
    <Link href={`/products/${product.id}`}>
      <Card className="group overflow-hidden hover:shadow-lg transition-shadow duration-200">
        <div className="relative aspect-square bg-zinc-100 overflow-hidden">
          {product.primaryImageUrl ? (
            <Image
              src={product.primaryImageUrl}
              alt={product.name}
              fill
              className="object-cover group-hover:scale-105 transition-transform duration-300"
            />
          ) : (
            <div className="w-full h-full flex items-center justify-center text-zinc-400">
              <svg className="w-16 h-16" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
              </svg>
            </div>
          )}
        </div>
        <CardContent className="p-4">
          {product.categoryName && (
            <Badge variant="secondary" className="mb-2 text-xs">{product.categoryName}</Badge>
          )}
          <h3 className="font-semibold text-sm leading-tight line-clamp-2 mb-1">{product.name}</h3>
          <p className="text-xs text-zinc-500 mb-2">{product.variantCount} variação{product.variantCount !== 1 ? "ões" : ""}</p>
          <p className="font-bold text-base">{formatPrice(product.basePrice, product.currency)}</p>
        </CardContent>
      </Card>
    </Link>
  );
}
