import { apiFetch } from "./client";
import type { Cart, AddToCartRequest } from "@/types";

export const getCart = (token: string): Promise<Cart> =>
  apiFetch<Cart>("/api/v1/cart", { headers: { Authorization: `Bearer ${token}` } });

export const addToCart = (request: AddToCartRequest, token: string): Promise<{ cartId: string }> =>
  apiFetch("/api/v1/cart/items", {
    method: "POST",
    body: JSON.stringify(request),
    headers: { Authorization: `Bearer ${token}` },
  });

export const removeFromCart = (itemId: string, token: string): Promise<void> =>
  apiFetch(`/api/v1/cart/items/${itemId}`, {
    method: "DELETE",
    headers: { Authorization: `Bearer ${token}` },
  });

export const clearCart = (token: string): Promise<void> =>
  apiFetch("/api/v1/cart", {
    method: "DELETE",
    headers: { Authorization: `Bearer ${token}` },
  });
