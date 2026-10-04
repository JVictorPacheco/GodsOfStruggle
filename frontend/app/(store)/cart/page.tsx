"use client";

import Link from "next/link";
import { useCart } from "@/contexts/CartContext";
import type { CartItem } from "@/types";

function formatPrice(amount: number, currency: string) {
  return new Intl.NumberFormat("pt-BR", { style: "currency", currency }).format(amount);
}

function CartItemRow({ item, onRemove }: { item: CartItem; onRemove: (id: string) => void }) {
  return (
    <div className="flex items-center gap-4 py-5 border-b border-white/10">
      <div className="flex-1 min-w-0">
        <p className="font-semibold text-white truncate">{item.productName}</p>
        {item.variantDescription && (
          <p className="text-sm text-zinc-500 mt-0.5">{item.variantDescription}</p>
        )}
        <p className="text-sm text-zinc-400 mt-1">
          {formatPrice(item.unitPrice, item.currency)} × {item.quantity}
        </p>
      </div>
      <div className="text-right shrink-0">
        <p className="font-bold text-orange-400">{formatPrice(item.totalPrice, item.currency)}</p>
        <button
          onClick={() => onRemove(item.id)}
          className="text-xs text-zinc-600 hover:text-red-500 transition-colors mt-1"
        >
          Remover
        </button>
      </div>
    </div>
  );
}

export default function CartPage() {
  const { cart, loading, removeItem, clearCart } = useCart();

  const handleRemove = (itemId: string) => {
    removeItem(itemId, "");
  };

  const handleClear = () => {
    clearCart("");
  };

  if (loading) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-16">
        <div className="space-y-4">
          {[1, 2, 3].map((i) => (
            <div key={i} className="h-20 bg-zinc-900 rounded-xl animate-pulse" />
          ))}
        </div>
      </div>
    );
  }

  if (!cart || cart.items.length === 0) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-24 text-center">
        <svg className="w-16 h-16 mx-auto text-zinc-700 mb-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 7H4l1-7z" />
        </svg>
        <h2 className="text-xl font-bold text-white mb-2">Seu carrinho está vazio</h2>
        <p className="text-zinc-500 mb-8">Adicione produtos para continuar</p>
        <Link
          href="/products"
          className="inline-flex items-center gap-2 bg-orange-500 hover:bg-orange-400 text-black font-bold px-6 py-3 rounded-full text-sm uppercase tracking-wide transition-colors"
        >
          Ver Produtos
        </Link>
      </div>
    );
  }

  return (
    <div className="max-w-2xl mx-auto px-4 sm:px-6 py-10">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-2xl font-black text-white">
          Carrinho <span className="text-orange-500">({cart.itemCount})</span>
        </h1>
        <button
          onClick={handleClear}
          className="text-sm text-zinc-600 hover:text-red-500 transition-colors"
        >
          Limpar tudo
        </button>
      </div>

      <div className="mb-8">
        {cart.items.map((item) => (
          <CartItemRow key={item.id} item={item} onRemove={handleRemove} />
        ))}
      </div>

      <div className="bg-zinc-900 rounded-xl p-6 border border-white/10">
        <div className="flex items-center justify-between mb-6">
          <span className="text-zinc-400">Total</span>
          <span className="text-2xl font-black text-white">
            {formatPrice(cart.total, cart.currency)}
          </span>
        </div>
        <button
          className="w-full py-4 rounded-xl font-bold text-sm uppercase tracking-wide bg-orange-500 hover:bg-orange-400 text-black transition-colors"
        >
          Finalizar Compra
        </button>
        <Link
          href="/products"
          className="block text-center text-sm text-zinc-500 hover:text-zinc-300 transition-colors mt-4"
        >
          Continuar comprando
        </Link>
      </div>
    </div>
  );
}
