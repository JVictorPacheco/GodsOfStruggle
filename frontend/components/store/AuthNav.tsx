"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { useAuth } from "@/contexts/AuthContext";

export function AuthNav() {
  const { user, loading, signOut } = useAuth();
  const router = useRouter();

  if (loading) return <span className="w-10 h-4 bg-zinc-800 rounded animate-pulse" />;

  if (!user) {
    return (
      <Link href="/login" className="text-zinc-400 hover:text-orange-500 transition-colors text-sm font-medium">
        Entrar
      </Link>
    );
  }

  const label = user.email?.split("@")[0] ?? "conta";

  return (
    <div className="flex items-center gap-3">
      <span className="text-xs text-zinc-500 hidden sm:block">{label}</span>
      <button
        onClick={async () => { await signOut(); router.push("/"); }}
        className="text-zinc-400 hover:text-orange-500 transition-colors text-sm font-medium"
      >
        Sair
      </button>
    </div>
  );
}
