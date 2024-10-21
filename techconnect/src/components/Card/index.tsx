import { useContext, useState } from "react";
import { FaRegHeart, FaRegComment, FaHeart } from "react-icons/fa";
import { postType } from "@/Types";
import ContextMenuCardPost from "./ContextMenuCardPost";
import { AuthContext } from "@/providers/AuthProvider";
import Link from "next/link";
import { CommentModal } from "../modal";
import { useNews } from "@/hooks/newsHooks/useNews";
import { useLikePost } from "@/hooks/likeHooks/useLikePost";
import { useDislikePost } from "@/hooks/likeHooks/useDislikePost";
import { useGetLikeByPost } from "@/hooks/likeHooks/useGetLikeByPost";
import Image from "next/image";
import moment from "moment";
import { motion } from "framer-motion";

export const CardPost: React.FC<postType> = (publicacao) => {
  const { userGlobalData } = useContext(AuthContext);

  const { mutate: likePost } = useLikePost();
  const { mutate: dislikePost } = useDislikePost();

  const { data: curtidas } = useGetLikeByPost(publicacao.idPublicacao);

  const handleLikePost = () => {
    likePost({
      postId: publicacao.idPublicacao,
      userId: userGlobalData?.idUsuario || "",
    });
  };

  const handleDisLikePost = () => {
    if (curtidas) {
      const idCurtida = curtidas?.find(
        (curtida) =>
          curtida.usuario.idUsuario === userGlobalData?.idUsuario &&
          curtida.publicacao.idPublicacao === publicacao.idPublicacao
      )?.idCurtida;
      dislikePost(idCurtida!);
    }
  };

  const handleTextLength = () => {
    setIsTextFull(!isTextFull);
  };

  const [isTextFull, setIsTextFull] = useState(false);

  return (
    <motion.div
      initial={{ opacity: 0, x: -100 }}
      whileInView={{ opacity: 1, x: 0 }}
      exit={{ opacity: 1, x: -100 }}
      transition={{ duration: 0.5 }}
      className="p-6 w-full h-full border rounded shadow-custom"
    >
      {/* header */}
      <motion.div
        initial={{ opacity: 0, x: -100 }}
        whileInView={{ opacity: 1, x: 0 }}
        exit={{ opacity: 1, x: -100 }}
        transition={{ duration: 0.6 }}
        className="border-b-2 pb-4 flex w-full h-16 items-center text-complementary-blue"
      >
        <div className="flex w-full h-full gap-4">
          <Link
            href={`
             profile/${publicacao.usuario.idUsuario}`}
            className="h-full flex flex-col !justify-center items-center w-max  lg:w-[50px] lg:h-[50px]"
          >
            <Image
              className="w-6 h-6 rounded lg:w-full lg:h-[300px]"
              src={publicacao.usuario.urlMidia || ""}
              alt="Imagem do usuário que fez o post."
              width={100}
              height={100}
            />
          </Link>

          <div className="flex flex-col">
            <span className="text-complementary-blue font-semibold">
              {publicacao.usuario.nome}
            </span>
            <span className="text-complementary-black font-medium">
              {moment(publicacao.dataPublicacao).format("DD/MM/YYYY")}
            </span>
          </div>
        </div>
        {userGlobalData?.idUsuario === publicacao.usuario.idUsuario && (
          <ContextMenuCardPost
            userId={publicacao.usuario.idUsuario}
            postId={publicacao.idPublicacao}
          />
        )}
      </motion.div>

      {/* body */}
      <div className="mt-3 flex items-center flex-col w-full h-max gap-5 overflow-hidden border-b-2 pb-5 lg:flex-col-reverse">
        <p className="text-sm w-full h-full text-justify transition-all ease-in-out duration-300 overflow-auto">
          {publicacao.descricao.length > 80 ? (
            <span className="max-w-full">
              {!isTextFull
                ? publicacao.descricao.substring(0, 80)
                : publicacao.descricao}
              {!isTextFull && "... "}

              <button
                onClick={handleTextLength}
                className="text-complementary-blue"
              >
                {!isTextFull ? "ver mais" : "ver menos"}
              </button>
            </span>
          ) : (
            <span className="font-medium">{publicacao.descricao}</span>
          )}
        </p>
        <motion.div
          initial={{ opacity: 0, scale: 0 }}
          whileInView={{ opacity: 1, scale: 1 }}
          exit={{ opacity: 0, scale: 0 }}
          transition={{ duration: 0.5 }}
          className="flex w-full justify-center"
        >
          <Image
            width={1000}
            height={1000}
            className="rounded w-full h-[200px] md:h-[400px]"
            src={publicacao.urlMidia}
            alt="Imagem postado pelo usuário."
          />
        </motion.div>
      </div>

      {/* footer */}
      <div className="flex w-full items-center h-max gap-2 mt-2">
        <button
          onClick={() =>
            !curtidas?.some(
              (curtida) =>
                curtida.usuario.idUsuario === userGlobalData?.idUsuario
            )
              ? handleLikePost()
              : handleDisLikePost()
          }
        >
          {curtidas?.some(
            (curtida) => curtida.usuario.idUsuario === userGlobalData?.idUsuario
          ) ? (
            <FaHeart className="text-complementary-blue" />
          ) : (
            <FaRegHeart />
          )}
        </button>

        <span className="text-xs font-medium">
          {curtidas && curtidas.length > 1
            ? `${curtidas.length} curtidas`
            : curtidas && curtidas.length === 1
            ? "1 curtida"
            : "0 curtidas"}
        </span>

        <CommentModal
          publicacao={publicacao}
          trigger={
            <FaRegComment className="transition-all ease-in-out duration-300 hover:text-complementary-blue" />
          }
        />
      </div>
    </motion.div>
  );
};

export const CardNews = ({ isProfilePage }: { isProfilePage?: boolean }) => {
  const { data: newsData } = useNews();

  return (
    <motion.aside
      initial={{ top: -100 }}
      animate={{ top: 0 }}
      transition={{ duration: 0.7 }}
      className={`hidden lg:flex h-full p-5 border rounded shadow-custom ${
        isProfilePage ? "mt-0 w-[30%]" : "mt-16 w-[40%]"
      }  lg:flex-col`}
    >
      {/* header */}
      <div className="w-full h-full flex justify-center text-lg font-semibold border-b-[1px] border-black pb-7">
        <span className="text-complementary-blue self-center">Novidades</span>
      </div>

      {/* body */}
      {newsData &&
        newsData.map((element, index) => (
          <motion.div
            initial={{ opacity: 0, x: -100 }}
            whileInView={{ opacity: 1, x: 0 }}
            exit={{ opacity: 1, x: -100 }}
            transition={{ duration: 0.2, delay: 0.5 + index * 0.2 }}
            className={`mt-2 flex flex-col ${
              newsData.length - index !== 1 && "border-b-[1px] border-black"
            }  pb-5`}
            key={index}
          >
            <span className="text-complementary-blue font-bold">
              O que aconteceu?
            </span>

            <a
              href={element.url}
              className="transition-all ease-in-out duration-300 hover:text-complementary-blue"
              target="_blank"
              rel="noopener noreferrer"
            >
              {element.title.substring(0, 50)}...
            </a>
          </motion.div>
        ))}
    </motion.aside>
  );
};
