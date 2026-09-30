import Link from "next/link";

export default function StoreLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="min-h-screen flex flex-col bg-white">
      <header className="border-b sticky top-0 bg-white z-10">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between h-16">
            <Link href="/" className="flex items-center gap-2">
              <span className="font-black text-xl tracking-tight">GODS OF STRUGGLE</span>
            </Link>
            <nav className="flex items-center gap-6 text-sm font-medium">
              <Link href="/products" className="text-zinc-600 hover:text-zinc-900 transition-colors">Produtos</Link>
              <Link href="/cart" className="relative text-zinc-600 hover:text-zinc-900 transition-colors">
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 7H4l1-7z" />
                </svg>
              </Link>
            </nav>
          </div>
        </div>
      </header>
      <main className="flex-1">{children}</main>
      <footer className="border-t py-8 text-center text-sm text-zinc-400">
        © {new Date().getFullYear()} Gods of Struggle. Todos os direitos reservados.
      </footer>
    </div>
  );
}
