"use client";

import Link from "next/link";
import { useCart } from "@/contexts/CartContext";

export function CartNav() {
  const { itemCount } = useCart();

  return (
    <Link href="/cart" className="relative text-zinc-400 hover:text-orange-500 transition-colors">
      <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 7H4l1-7z" />
      </svg>
      {itemCount > 0 && (
        <span className="absolute -top-2 -right-2 bg-orange-500 text-black text-[10px] font-black rounded-full w-4 h-4 flex items-center justify-center leading-none">
          {itemCount > 9 ? "9+" : itemCount}
        </span>
      )}
    </Link>
  );
}
