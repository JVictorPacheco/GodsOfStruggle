import { apiFetch } from "./client";
import type { PagedResult, Product, ProductListItem } from "@/types";

export interface GetProductsParams {
  search?: string;
  categoryId?: string;
  minPrice?: number;
  maxPrice?: number;
  page?: number;
  pageSize?: number;
}

export function getProducts(params: GetProductsParams = {}): Promise<PagedResult<ProductListItem>> {
  const query = new URLSearchParams();
  if (params.search) query.set("search", params.search);
  if (params.categoryId) query.set("categoryId", params.categoryId);
  if (params.minPrice != null) query.set("minPrice", String(params.minPrice));
  if (params.maxPrice != null) query.set("maxPrice", String(params.maxPrice));
  query.set("page", String(params.page ?? 1));
  query.set("pageSize", String(params.pageSize ?? 20));
  return apiFetch<PagedResult<ProductListItem>>(`/api/v1/products?${query}`);
}

export function getProductById(id: string): Promise<Product> {
  return apiFetch<Product>(`/api/v1/products/${id}`);
}
