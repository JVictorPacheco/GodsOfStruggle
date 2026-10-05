"use client";

import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useAuth } from "@/contexts/AuthContext";

export default function RegisterPage() {
  const { signUp } = useAuth();
  const router = useRouter();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirm, setConfirm] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    if (password !== confirm) {
      setError("As senhas não coincidem.");
      return;
    }
    if (password.length < 6) {
      setError("A senha deve ter pelo menos 6 caracteres.");
      return;
    }
    setLoading(true);
    const { error } = await signUp(email, password);
    setLoading(false);
    if (error) {
      setError(error.message ?? "Erro ao criar conta. Tente novamente.");
      return;
    }
    setSuccess(true);
  };

  if (success) {
    return (
      <div className="min-h-[calc(100vh-8rem)] flex items-center justify-center px-4">
        <div className="w-full max-w-sm text-center">
          <div className="w-16 h-16 rounded-full bg-orange-500/10 flex items-center justify-center mx-auto mb-6">
            <svg className="w-8 h-8 text-orange-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
            </svg>
          </div>
          <h2 className="text-2xl font-black text-white mb-2">Conta criada!</h2>
          <p className="text-zinc-500 text-sm mb-8">
            Verifique seu e-mail para confirmar o cadastro e depois faça login.
          </p>
          <button
            onClick={() => router.push("/login")}
            className="w-full h-11 rounded-xl font-bold text-sm uppercase tracking-wide bg-orange-500 hover:bg-orange-400 text-black transition-colors"
          >
            Ir para Login
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-[calc(100vh-8rem)] flex items-center justify-center px-4">
      <div className="w-full max-w-sm">
        <h1 className="text-3xl font-black text-white mb-2">Criar conta</h1>
        <p className="text-zinc-500 mb-8 text-sm">
          Já tem conta?{" "}
          <Link href="/login" className="text-orange-500 hover:text-orange-400 font-medium">
            Entrar
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
              autoComplete="new-password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Mínimo 6 caracteres"
              className="w-full h-11 rounded-xl border border-white/10 bg-zinc-900 px-4 text-white placeholder-zinc-600 focus:outline-none focus:border-orange-500 transition-colors text-sm"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-zinc-300 mb-1.5">Confirmar senha</label>
            <input
              type="password"
              required
              autoComplete="new-password"
              value={confirm}
              onChange={(e) => setConfirm(e.target.value)}
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
            {loading ? "Criando conta..." : "Criar conta"}
          </button>
        </form>
      </div>
    </div>
  );
}
