import type { Metadata } from "next";
import "./globals.css";
import { Montserrat_Alternates } from "@next/font/google";
import Providers from "@/providers";
import { cn } from "@/lib/utils";

// Importando a fonte Montserrat Alternates
const montserrat = Montserrat_Alternates({
  subsets: ["latin"],
  weight: ["100", "200", "300", "400", "500", "600", "700", "800", "900"],
  variable: "--montserrat-alternates",
});

export const metadata: Metadata = {
  title: "Tech Connect",
  description: "A rede social dos desenvolvedores!",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html className={cn(`${montserrat.variable} antialiased`)} lang="pt-BR">
      <body>
        <Providers>{children}</Providers>
      </body>
    </html>
  );
}
