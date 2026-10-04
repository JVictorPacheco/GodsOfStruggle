"use client";

import { createContext, useContext, useState, useCallback, useEffect, type ReactNode } from "react";
import type { Cart, AddToCartRequest } from "@/types";
import * as cartApi from "@/lib/api/cart";

interface CartContextValue {
  cart: Cart | null;
  itemCount: number;
  loading: boolean;
  addItem: (request: AddToCartRequest, token: string) => Promise<void>;
  removeItem: (itemId: string, token: string) => Promise<void>;
  clearCart: (token: string) => Promise<void>;
  refreshCart: (token: string) => Promise<void>;
}

const CartContext = createContext<CartContextValue | null>(null);

export function CartProvider({ children }: { children: ReactNode }) {
  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(false);

  const refreshCart = useCallback(async (token: string) => {
    try {
      const data = await cartApi.getCart(token);
      setCart(data);
    } catch {
      setCart(null);
    }
  }, []);

  const addItem = useCallback(async (request: AddToCartRequest, token: string) => {
    setLoading(true);
    try {
      await cartApi.addToCart(request, token);
      await refreshCart(token);
    } finally {
      setLoading(false);
    }
  }, [refreshCart]);

  const removeItem = useCallback(async (itemId: string, token: string) => {
    setLoading(true);
    try {
      await cartApi.removeFromCart(itemId, token);
      await refreshCart(token);
    } finally {
      setLoading(false);
    }
  }, [refreshCart]);

  const clear = useCallback(async (token: string) => {
    setLoading(true);
    try {
      await cartApi.clearCart(token);
      setCart(null);
    } finally {
      setLoading(false);
    }
  }, []);

  return (
    <CartContext.Provider value={{
      cart,
      itemCount: cart?.itemCount ?? 0,
      loading,
      addItem,
      removeItem,
      clearCart: clear,
      refreshCart,
    }}>
      {children}
    </CartContext.Provider>
  );
}

export function useCart() {
  const ctx = useContext(CartContext);
  if (!ctx) throw new Error("useCart deve ser usado dentro de CartProvider");
  return ctx;
}
