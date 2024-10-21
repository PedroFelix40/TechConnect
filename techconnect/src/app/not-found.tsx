"use client";
import { useRouter } from "next/navigation";
import Image from "next/image";
import Main from "@/components/Main";

const NotFound = () => {
  const router = useRouter();
  const homePageURl = `/`;

  // useEffect(() => {
  //   router.replace(homePageURl);
  // }, [router]);
  // router.replace(homePageURl);

  const goToHome = () => {
    router.replace(homePageURl);
  };

  const imageSize = window.innerWidth < 768 ? 250 : 500;

  return (
    <Main isAuthPage>
      <section className="bg-blueGradient w-full h-full flex flex-col items-center justify-evenly">
        <h1 className="text-complementary-white font-bold text-4xl">
          Página não encontrada
        </h1>

        <button
          onClick={goToHome}
          className="text-complementary-blue p-2 border border-complementary-white rounded bg-white transition-all ease-in-out duration-300 hover:scale-110"
        >
          Voltar
        </button>

        <Image
          width={imageSize}
          height={imageSize}
          src={"/icons/not-found.svg"}
          alt="Imagem de página não encontrada."
        />
      </section>
    </Main>
  );
};

export default NotFound;
