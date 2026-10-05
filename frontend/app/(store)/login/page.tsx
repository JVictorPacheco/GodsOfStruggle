"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter, useSearchParams } from "next/navigation";
import { useAuth } from "@/contexts/AuthContext";

export default function LoginPage() {
  const { signIn } = useAuth();
  const router = useRouter();
  const searchParams = useSearchParams();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const redirectTo = searchParams.get("redirect") ?? "/";

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);
    const { error } = await signIn(email, password);
    setLoading(false);
    if (error) {
      setError("E-mail ou senha incorretos.");
      return;
    }
    router.push(redirectTo);
  };

  return (
    <div className="min-h-[calc(100vh-8rem)] flex items-center justify-center px-4">
      <div className="w-full max-w-sm">
        <h1 className="text-3xl font-black text-white mb-2">Entrar</h1>
        <p className="text-zinc-500 mb-8 text-sm">
          Novo por aqui?{" "}
          <Link href="/register" className="text-orange-500 hover:text-orange-400 font-medium">
            Criar conta
          </Link>
        </p>

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-sm font-medium text-zinc-300 mb-1.5">E-mail</label>
            <input
              type="email"
              required
              autoComplete="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="seu@email.com"
              className="w-full h-11 rounded-xl border border-white/10 bg-zinc-900 px-4 text-white placeholder-zinc-600 focus:outline-none focus:border-orange-500 transition-colors text-sm"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-zinc-300 mb-1.5">Senha</label>
            <input
              type="password"
              required
              autoComplete="current-password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
              className="w-full h-11 rounded-xl border border-white/10 bg-zinc-900 px-4 text-white placeholder-zinc-600 focus:outline-none focus:border-orange-500 transition-colors text-sm"
            />
          </div>

          {error && <p className="text-red-500 text-sm">{error}</p>}

          <button
            type="submit"
            disabled={loading}
            className="w-full h-11 rounded-xl font-bold text-sm uppercase tracking-wide bg-orange-500 hover:bg-orange-400 text-black transition-colors disabled:bg-zinc-800 disabled:text-zinc-600 disabled:cursor-not-allowed mt-2"
          >
            {loading ? "Entrando..." : "Entrar"}
          </button>
        </form>
      </div>
    </div>
  );
}
