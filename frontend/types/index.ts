export interface Category {
  id: string;
  name: string;
  description: string;
  slug: string;
  parentCategoryId: string | null;
  isActive: boolean;
}

export interface ProductVariant {
  id: string;
  sku: string;
  attributes: Record<string, string>;
  price: number;
  currency: string;
  stockQuantity: number;
  isActive: boolean;
}

export interface ProductImage {
  id: string;
  url: string;
  altText: string;
  displayOrder: number;
  isPrimary: boolean;
}

export interface Product {
  id: string;
  name: string;
  description: string;
  basePrice: number;
  currency: string;
  categoryId: string;
  categoryName: string | null;
  images: ProductImage[];
  variants: ProductVariant[];
  isActive: boolean;
  createdAt: string;
}

export interface ProductListItem {
  id: string;
  name: string;
  basePrice: number;
  currency: string;
  categoryId: string;
  categoryName: string | null;
  primaryImageUrl: string | null;
  variantCount: number;
  isActive: boolean;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
