import Link from "next/link";
import { getProducts } from "@/lib/api/products";
import { ProductCard } from "@/components/store/ProductCard";
import { Button } from "@/components/ui/button";

export default async function HomePage() {
  const featured = await getProducts({ pageSize: 4 }).catch(() => null);

  return (
    <div>
      <section className="bg-zinc-950 text-white py-24 px-4">
        <div className="max-w-4xl mx-auto text-center">
          <h1 className="text-5xl font-black tracking-tight mb-4">
            ELEVE SEU JOGO.<br />DOMINE O TATAME.
          </h1>
          <p className="text-zinc-400 text-lg mb-8">
            Kimonos e equipamentos premium para atletas que levam as artes marciais a sério.
          </p>
          <Button asChild size="lg" className="bg-white text-black hover:bg-zinc-100">
            <Link href="/products">Ver Produtos</Link>
          </Button>
        </div>
      </section>

      {featured && featured.items.length > 0 && (
        <section className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-16">
          <div className="flex items-center justify-between mb-8">
            <h2 className="text-2xl font-bold">Produtos em Destaque</h2>
            <Link href="/products" className="text-sm text-zinc-500 hover:text-zinc-900 transition-colors">
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
    </div>
  );
}
