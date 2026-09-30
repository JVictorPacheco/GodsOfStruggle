"use client";

import { Badge } from "@/components/ui/badge";
import type { Category } from "@/types";

interface CategoryFilterProps {
  categories: Category[];
  selected: string | null;
  onChange: (id: string | null) => void;
}

export function CategoryFilter({ categories, selected, onChange }: CategoryFilterProps) {
  return (
    <div className="flex flex-wrap gap-2">
      <Badge
        variant={selected === null ? "default" : "outline"}
        className="cursor-pointer px-3 py-1"
        onClick={() => onChange(null)}
      >
        Todos
      </Badge>
      {categories.map((cat) => (
        <Badge
          key={cat.id}
          variant={selected === cat.id ? "default" : "outline"}
          className="cursor-pointer px-3 py-1"
          onClick={() => onChange(cat.id)}
        >
          {cat.name}
        </Badge>
      ))}
    </div>
  );
}
