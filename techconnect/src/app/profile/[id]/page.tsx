"use client";
import { CardPost } from "@/components/Card";
import Header from "@/components/Header";
import { useGetPostsByUserId } from "@/hooks/postHooks/useGetPostsByUserId";
import { useGetUserById } from "@/hooks/userHooks/useGetUserById";
import Image from "next/image";
import React, { useContext, useEffect } from "react";
import { FollowersModal } from "@/components/modal";
import {
  FollowButton,
  MessageButton,
  ProfileButton,
} from "@/components/button";
import { AuthContext } from "@/providers/AuthProvider";
import { useFollowUser } from "@/hooks/followHooks/useFollowUser";
import { useUnFollowUser } from "@/hooks/followHooks/useUnFollowUser";
import { useGetFollowedsById } from "@/hooks/followHooks/useGetFollowedsById";
import { useGetFollowersById } from "@/hooks/followHooks/useGetFollowersById";
import { useGetUniqueChatByUsersId } from "@/hooks/chatHooks/useGetUniqueChatByUsersId";
import { useRouter } from "next/navigation";
import { useCreateChat } from "@/hooks/chatHooks/useCreateChat";
import { motion } from "framer-motion";
import Main from "@/components/Main";

interface routeParams {
  params: { id: string };
}

const ProfilePage = ({ params: { id: userIdParam } }: routeParams) => {
  const { userGlobalData } = useContext(AuthContext);

  const router = useRouter();

  const { data: posts } = useGetPostsByUserId(userIdParam);

  const { data: user } = useGetUserById(userIdParam);

  const { data: chatBuscado } = useGetUniqueChatByUsersId({
    userId1: userGlobalData?.idUsuario || "",
    userId2: userIdParam,
  });

  const { mutate: createChatMutate, isSuccess: isSuccessCreateChat } =
    useCreateChat();

  const { mutate: followUserMutate } = useFollowUser(userIdParam);

  const { mutate: unFollowUserMutate } = useUnFollowUser(userIdParam);

  const { data: followedUsers } = useGetFollowedsById(userIdParam);

  const { data: followUsers } = useGetFollowersById(userIdParam);

  const handleFollowUser = () => {
    followUserMutate({
      followerId: userGlobalData?.idUsuario || "",
      followedId: userIdParam,
    });
  };

  const handleUnFollowUser = () => {
    unFollowUserMutate({
      idSeguidor: userGlobalData?.idUsuario || "",
      idSeguido: userIdParam,
    });
  };

  const handleSendMessage = () => {
    if (chatBuscado) {
      router.push(`/chat`);
      return;
    }
    createChatMutate({
      idUsuario1: userGlobalData?.idUsuario || "",
      idUsuario2: userIdParam,
    });
  };

  useEffect(() => {
    if (isSuccessCreateChat) {
      router.push(`/chat`);
    }
  }, [isSuccessCreateChat]);

  return (
    <>
      <Header />

      <Main>
        <div className="w-[80%] h-full mt-[50px] flex justify-center pb-20">
          <section className="w-full h-full flex flex-col lg:flex-row lg:items-start lg:justify-between items-center lg:w-full">
            <motion.article
              initial={{ opacity: 0, x: -500 }}
              whileInView={{ opacity: 1, x: 0 }}
              exit={{ opacity: 0, x: -500 }}
              transition={{ duration: 0.9 }}
              className=" border-complementary-black w-full h-full flex flex-col items-center gap-8 font-medium lg:w-[40%]"
            >
              <Image
                width={100}
                height={100}
                className="w-1/2 rounded-xl lg:h-full "
                src={user?.urlMidia || ""}
                alt="Imagem do usuário que fez o post."
              />

              <span className="pb-5 w-full text-xl text-center font-semibold border-b-[1px] border-complementary-black">
                {user?.nome}
              </span>
              <div className="flex flex-col gap-3 w-full">
                {userGlobalData?.idUsuario !== userIdParam && (
                  <FollowButton
                    isFollowing={
                      followUsers
                        ? followUsers.some(
                            (user) =>
                              user.idUsuario === userGlobalData?.idUsuario
                          )
                        : false
                    }
                    onClick={
                      followUsers?.some(
                        (user) => user.idUsuario === userGlobalData?.idUsuario
                      )
                        ? handleUnFollowUser
                        : handleFollowUser
                    }
                  />
                )}
                {userGlobalData?.idUsuario !== userIdParam && (
                  <MessageButton onClick={handleSendMessage} />
                )}

                <FollowersModal
                  userIdSearched={userIdParam}
                  isFollow
                  users={followUsers ? followUsers : []}
                  trigger={
                    <ProfileButton
                      label="Seguidores"
                      number={followUsers ? followUsers.length : 0}
                    />
                  }
                />

                <FollowersModal
                  userIdSearched={userIdParam}
                  users={followedUsers ? followedUsers : []}
                  trigger={
                    <ProfileButton
                      label="Seguindo"
                      number={followedUsers ? followedUsers.length : 0}
                    />
                  }
                />
              </div>
            </motion.article>
            <div
              className={`flex flex-col w-full mt-10 lg:mt-0 h-full lg:w-2/4 gap-10`}
            >
              {posts && posts.length > 0 ? (
                posts.map((element) => (
                  <CardPost
                    dataPublicacao={element.dataPublicacao}
                    descricao={element.descricao}
                    idPublicacao={element.idPublicacao}
                    urlMidia={element.urlMidia}
                    usuario={element.usuario}
                    key={element.idPublicacao}
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

          {/* <CardNews isProfilePage /> */}
        </div>
      </Main>
    </>
  );
};

export default ProfilePage;
