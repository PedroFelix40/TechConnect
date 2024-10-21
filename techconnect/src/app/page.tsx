"use client";
import React from "react";
import { CardNews, CardPost } from "@/components/Card";
import Header from "@/components/Header";
import { PostModal } from "@/components/modal";
import { useGetAllPosts } from "@/hooks/postHooks/useGetAllPosts";
import Image from "next/image";
import Main from "@/components/Main";

export default function Home() {
  const { data: posts } = useGetAllPosts();

  return (
    <>
      <Header />
      <Main>
        <div className="w-[80%] h-full mt-[50px] flex justify-center lg:justify-between pb-20">
          <section className=" h-full flex flex-col items-center w-full md:w-[50%]">
            <PostModal trigger={"+ Publicar ideias"} />

            <div className={`flex flex-col w-full h-full mt-8 gap-10`}>
              {posts ? (
                posts.map((element) => (
                  <CardPost
                    key={element.idPublicacao}
                    dataPublicacao={element.dataPublicacao}
                    descricao={element.descricao}
                    idPublicacao={element.idPublicacao}
                    urlMidia={element.urlMidia}
                    usuario={element.usuario}
                  />
                ))
              ) : (
                <div className="w-full h-full flex flex-col justify-center items-center mt-20">
                  <span className="text-xl text-center">
                    Nenhum post encontrado.
                  </span>
                  <Image
                    width={500}
                    height={500}
                    src={"/icons/not-found.svg"}
                    alt="Imagem de página não encontrada."
                  />
                </div>
              )}
            </div>
          </section>

          <CardNews />
        </div>
      </Main>
    </>
  );
}
