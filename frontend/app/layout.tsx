import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import Link from "next/link";
import Image from "next/image";
import { AuthProvider } from "@/contexts/AuthContext";
import { CartProvider } from "@/contexts/CartContext";
import { CartNav } from "@/components/store/CartNav";
import { AuthNav } from "@/components/store/AuthNav";
import "./globals.css";

const geistSans = Geist({ variable: "--font-geist-sans", subsets: ["latin"] });
const geistMono = Geist_Mono({ variable: "--font-geist-mono", subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Gods of Struggle — Kimonos e Equipamentos de Luta",
  description: "A melhor loja de kimonos e equipamentos para artes marciais.",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-BR" className={`${geistSans.variable} ${geistMono.variable} h-full antialiased`}>
      <body className="min-h-full flex flex-col bg-[#0a0a0a] text-white">
        <AuthProvider>
          <CartProvider>
            <header className="border-b border-white/10 sticky top-0 bg-[#0a0a0a]/95 backdrop-blur z-50">
              <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <div className="flex items-center justify-between h-16">
                  <Link href="/" className="flex items-center gap-3">
                    <Image
                      src="/logo.jpeg"
                      alt="Gods of Struggle"
                      width={44}
                      height={44}
                      className="rounded-full object-cover"
                      priority
                    />
                    <span className="font-black text-lg tracking-tight hidden sm:block">
                      <span className="text-white">GODS OF</span>{" "}
                      <span className="text-orange-500">STRUGGLE</span>
                    </span>
                  </Link>
                  <nav className="flex items-center gap-6 text-sm font-medium">
                    <Link href="/products" className="text-zinc-400 hover:text-orange-500 transition-colors">
                      Produtos
                    </Link>
                    <CartNav />
                    <AuthNav />
                  </nav>
                </div>
              </div>
            </header>

            <main className="flex-1">{children}</main>

            <footer className="border-t border-white/10 py-8 text-center text-sm text-zinc-500">
              © {new Date().getFullYear()} Gods of Struggle. Todos os direitos reservados.
            </footer>
          </CartProvider>
        </AuthProvider>
      </body>
    </html>
  );
}
