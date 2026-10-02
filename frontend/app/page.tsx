import Link from "next/link";
import { getProducts } from "@/lib/api/products";
import { ProductCard } from "@/components/store/ProductCard";

export default async function HomePage() {
  const featured = await getProducts({ pageSize: 4 }).catch(() => null);

  return (
    <div>
      {/* Hero */}
      <section className="relative bg-[#0a0a0a] text-white py-28 px-4 overflow-hidden">
        <div className="absolute inset-0 bg-gradient-to-br from-blue-950/40 via-transparent to-orange-950/40 pointer-events-none" />
        <div className="relative max-w-4xl mx-auto text-center">
          <div className="inline-block px-3 py-1 rounded-full border border-orange-500/30 text-orange-400 text-xs font-semibold tracking-widest mb-6 uppercase">
            Equipamentos Premium
          </div>
          <h1 className="text-5xl md:text-6xl font-black tracking-tight mb-4 leading-tight">
            ELEVE SEU JOGO.<br />
            <span className="text-transparent bg-clip-text bg-gradient-to-r from-blue-400 to-orange-500">
              DOMINE O TATAME.
            </span>
          </h1>
          <p className="text-zinc-400 text-lg mb-10 max-w-2xl mx-auto">
            Kimonos e equipamentos premium para atletas que levam as artes marciais a sério.
          </p>
          <Link
            href="/products"
            className="inline-flex items-center gap-2 bg-orange-500 hover:bg-orange-400 text-black font-bold px-8 py-3 rounded-full transition-colors text-sm uppercase tracking-wide"
          >
            Ver Produtos
            <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
            </svg>
          </Link>
        </div>
      </section>

      {/* Featured Products */}
      {featured && featured.items.length > 0 && (
        <section className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-16">
          <div className="flex items-center justify-between mb-8">
            <h2 className="text-2xl font-bold text-white">Produtos em Destaque</h2>
            <Link href="/products" className="text-sm text-zinc-500 hover:text-orange-400 transition-colors">
              Ver todos →
            </Link>
          </div>
          <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
            {featured.items.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
        </section>
      )}

      {/* Categories banner */}
      <section className="border-t border-white/10 py-16 px-4">
        <div className="max-w-7xl mx-auto grid grid-cols-2 md:grid-cols-4 gap-4 text-center">
          {[
            { label: "Kimonos BJJ", icon: "🥋" },
            { label: "Kimonos Judô", icon: "🏯" },
            { label: "Rashguards", icon: "👕" },
            { label: "Acessórios", icon: "🎯" },
          ].map(({ label, icon }) => (
            <Link
              key={label}
              href={`/products?search=${encodeURIComponent(label)}`}
              className="group flex flex-col items-center gap-3 p-6 rounded-xl border border-white/10 hover:border-orange-500/50 hover:bg-orange-500/5 transition-all"
            >
              <span className="text-3xl">{icon}</span>
              <span className="text-sm font-semibold text-zinc-300 group-hover:text-orange-400 transition-colors">{label}</span>
            </Link>
          ))}
        </div>
      </section>
    </div>
  );
}
