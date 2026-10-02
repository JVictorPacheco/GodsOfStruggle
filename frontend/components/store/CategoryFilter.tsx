"use client";

import type { Category } from "@/types";

interface CategoryFilterProps {
  categories: Category[];
  selected: string | null;
  onChange: (id: string | null) => void;
}

export function CategoryFilter({ categories, selected, onChange }: CategoryFilterProps) {
  return (
    <div className="flex flex-wrap gap-2">
      <button
        onClick={() => onChange(null)}
        className={`px-4 py-1.5 rounded-full text-sm font-semibold border transition-all ${
          selected === null
            ? "bg-orange-500 text-black border-orange-500"
            : "border-white/10 text-zinc-400 hover:border-orange-500/50 hover:text-white"
        }`}
      >
        Todos
      </button>
      {categories.map((cat) => (
        <button
          key={cat.id}
          onClick={() => onChange(cat.id)}
          className={`px-4 py-1.5 rounded-full text-sm font-semibold border transition-all ${
            selected === cat.id
              ? "bg-orange-500 text-black border-orange-500"
              : "border-white/10 text-zinc-400 hover:border-orange-500/50 hover:text-white"
          }`}
        >
          {cat.name}
        </button>
      ))}
    </div>
  );
}
