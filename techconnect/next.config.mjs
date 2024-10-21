/** @type {import('next').NextConfig} */
const nextConfig = {
  //configuração para permitir que o Next/image renderize imagens vindas do google
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "lh3.googleusercontent.com",
        pathname: "/**", // Permite qualquer caminho após o domínio
      },
      {
        protocol: "https",
        hostname: "blobbibliotech.blob.core.windows.net", // Para imagens do Azure Blob Storage
        pathname: "/**", // Permite qualquer caminho após o domínio
      },
      {
        protocol: "https",
        hostname: "blobvitalhubfilipegoisg2.blob.core.windows.net", // Para imagens do Azure Blob Storage
        pathname: "/**", // Permite qualquer caminho após o domínio
      },
    ],
  },
};

export default nextConfig;
