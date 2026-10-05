"use client";

import { createContext, useContext, useState, useCallback, useEffect, type ReactNode } from "react";
import type { Cart, AddToCartRequest } from "@/types";
import * as cartApi from "@/lib/api/cart";
import { useAuth } from "@/contexts/AuthContext";

interface CartContextValue {
  cart: Cart | null;
  itemCount: number;
  loading: boolean;
  addItem: (request: AddToCartRequest) => Promise<void>;
  removeItem: (itemId: string) => Promise<void>;
  clearCart: () => Promise<void>;
}

const CartContext = createContext<CartContextValue | null>(null);

export function CartProvider({ children }: { children: ReactNode }) {
  const { session } = useAuth();
  const [cart, setCart] = useState<Cart | null>(null);
  const [loading, setLoading] = useState(false);

  const refreshCart = useCallback(async (token: string) => {
    if (!token) { setCart(null); return; }
    try {
      const data = await cartApi.getCart(token);
      setCart(data);
    } catch {
      setCart(null);
    }
  }, []);

  useEffect(() => {
    refreshCart(session?.access_token ?? "");
  }, [session, refreshCart]);

  const addItem = useCallback(async (request: AddToCartRequest) => {
    const token = session?.access_token;
    if (!token) return;
    setLoading(true);
    try {
      await cartApi.addToCart(request, token);
      await refreshCart(token);
    } finally {
      setLoading(false);
    }
  }, [session, refreshCart]);

  const removeItem = useCallback(async (itemId: string) => {
    const token = session?.access_token;
    if (!token) return;
    setLoading(true);
    try {
      await cartApi.removeFromCart(itemId, token);
      await refreshCart(token);
    } finally {
      setLoading(false);
    }
  }, [session, refreshCart]);

  const clear = useCallback(async () => {
    const token = session?.access_token;
    if (!token) return;
    setLoading(true);
    try {
      await cartApi.clearCart(token);
      setCart(null);
    } finally {
      setLoading(false);
    }
  }, [session]);

  return (
    <CartContext.Provider value={{
      cart,
      itemCount: cart?.itemCount ?? 0,
      loading,
      addItem,
      removeItem,
      clearCart: clear,
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
