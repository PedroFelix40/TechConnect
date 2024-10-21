import {
  useState,
  ReactNode,
  useEffect,
  useContext,
  ComponentProps,
} from "react";
import { IoSendSharp } from "react-icons/io5";
import {
  DialogHeader,
  Dialog,
  DialogContent,
  DialogDescription,
  DialogTitle,
  DialogTrigger,
} from "../ui/dialog";
import "./style.css";
import { ScrollArea } from "../ui/scroll-area";
import {
  CommentType,
  PostFormData,
  postType,
  schemaPost,
  userAuthType,
} from "@/Types";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { CommentFormData, schemaComment } from "@/Types";
import { useCreateComment } from "@/hooks/commentHooks/useCreateComment";
import Image from "next/image";
import { useCreatePost } from "@/hooks/postHooks/useCreatePost";
import { AuthContext } from "@/providers/AuthProvider";
import { useGetCommentsByPost } from "@/hooks/commentHooks/useGetCommentsByPost";
import Link from "next/link";
import { useUnFollowUser } from "@/hooks/followHooks/useUnFollowUser";
import { Toaster } from "../ui/toaster";
import { useToast } from "@/hooks/use-toast";
import { useDeleteComment } from "@/hooks/commentHooks/useDeleteComment";
import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "../ui/context-menu";
import { AnimatePresence, motion } from "framer-motion";

type ModalProps = {
  trigger: ReactNode;
};

type FollowersOrFollowedModalProps = ModalProps & {
  users: userAuthType[];
  userIdSearched: string;
  isFollow?: boolean;
};

type PostModalProps = ModalProps & {
  publicacao: postType;
};

export const PostModal = ({ trigger }: ModalProps) => {
  const [isOpen, setIsOpen] = useState(false);

  const { userGlobalData } = useContext(AuthContext);

  const {
    mutate: createPostMutate,
    isSuccess: isSuccessCreatePost,
    isError: isErrorCreatePost,
  } = useCreatePost();

  const { toast } = useToast();

  const {
    register,
    handleSubmit,
    watch,
    reset,
    formState: { errors },
  } = useForm<PostFormData>({
    resolver: zodResolver(schemaPost),
  });

  const [filePreview, setFilePreview] = useState<string | null>(null);

  const selectedFile = watch("file");

  const submitForm = ({ descricao, file }: PostFormData) => {
    const extractedFile = file[0];

    createPostMutate({
      Descricao: descricao,
      file: extractedFile,
      IdUsuario: userGlobalData!.idUsuario,
    });
  };

  const triggerStyle =
    "max-w-[220px] transition-all duration-500 ease-in-out hover:scale-110 text-center text-sm bg-complementary-blue px-10 py-2 text-complementary-white rounded-xl font-montserratAlternates font-bold lg:w-full lg:max-w-full";

  useEffect(() => {
    if (selectedFile && selectedFile.length) {
      setFilePreview(URL.createObjectURL(selectedFile[0]));
    }
  }, [selectedFile]);

  useEffect(() => {
    if (isErrorCreatePost) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: "A descrição não deve conter termos ofensivos!",
      });
    }
  }, [isErrorCreatePost]);

  useEffect(() => {
    if (errors.descricao) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: errors.descricao.message,
      });
    }
    if (errors.file) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: errors.file.message,
      });
    }
  }, [errors]);

  useEffect(() => {
    if (isSuccessCreatePost) {
      setIsOpen(false);
      setFilePreview(null);
      reset();
    }
  }, [isSuccessCreatePost]);

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger className={triggerStyle}>{trigger}</DialogTrigger>
      <DialogContent className="dialog-content">
        {/* grid */}
        <form
          onSubmit={handleSubmit(submitForm)}
          className="flex flex-col w-full md:w-5/6 h-full justify-between md:gap-4 "
        >
          <DialogHeader className="w-full h-full ">
            <DialogTitle className="mb-2 !mt-0 lg:!mb-0 font-montserrat text-complementary-blue text-lg font-semibold text-center">
              CRIAR NOVA PUBLICAÇÃO
            </DialogTitle>
            <DialogDescription className="opacity-0 h-0"></DialogDescription>

            <hr className="border-black-2 w-full self-center justify-self-center" />

            {/* body */}
            <div className="w-full h-full flex flex-col justify-center">
              <div className="flex flex-col md:flex-row justify-between h-full">
                <div className="relative flex flex-row justify-center items-center w-full md:w-[45%] md:h-full h-2/4 bg-[#D9D9D9] rounded-t-lg md:rounded-lg">
                  {filePreview && (
                    <Image
                      width={100}
                      height={100}
                      className="absolute z-10 object-cover w-full h-full md:h-1/2 cursor-pointer"
                      src={filePreview}
                      alt="Imagem selecionada pelo usuário."
                    />
                  )}

                  <div className="absolute z-20 bg-transparent flex flex-row justify-center items-center w-full md:h-full h-1/2 bg-[#D9D9D9] rounded-t-lg md:rounded-lg">
                    <input
                      className="opacity-0 w-full h-full"
                      type="file"
                      {...register("file")}
                    />
                    <p className="absolute w-max md:w-1/4 pointer-events-none text-center underline">
                      {selectedFile ? "Alterar imagem" : "Adicionar imagem"}
                    </p>
                  </div>
                </div>

                <div className="w-full md:w-[45%] h-1/2 md:h-full">
                  <textarea
                    {...register("descricao")}
                    placeholder="Adicione uma descrição..."
                    className="border border-black border-opacity-20 w-full h-full resize-none md:rounded-lg shadow-lg font-montserrat font-normal text-sm md:text-md .placeholder-gray-400 p-2 md:p-4"
                    cols={30}
                    rows={10}
                  />
                </div>
              </div>
            </div>
          </DialogHeader>
          <hr className="border-black-2 w-full md:block hidden" />
          <div className="flex flex-col items-end w-full h-max mt-[6px]">
            <button className="flex items-center w-max px-4 md:p-0 md:w-max md:bg-transparent bg-complementary-blue rounded-br-lg text-white md:text-complementary-blue text-lg md:text-xl font-semibold  md:text-right capitalize md:uppercase justify-center gap-3">
              Compartilhar
              <IoSendSharp
                className="min-h-8 min-w-6 md:block hidden"
                color="#0A66C2"
              />
            </button>
          </div>
        </form>
        <Toaster />
      </DialogContent>
    </Dialog>
  );
};

export const CommentModal = ({ trigger, publicacao }: PostModalProps) => {
  const [isOpen, setIsOpen] = useState(false);

  const { register, handleSubmit, watch, reset } = useForm<CommentFormData>({
    resolver: zodResolver(schemaComment),
  });

  const { toast } = useToast();

  const { userGlobalData } = useContext(AuthContext);

  const {
    mutate: createCommentMutate,
    isSuccess: isSuccessCreateComment,
    isError: isErrorCreateComment,
  } = useCreateComment(publicacao.idPublicacao);

  const { data: comentarios } = useGetCommentsByPost(publicacao.idPublicacao);

  const comment = watch("descricao");

  const createComment = ({ descricao }: CommentFormData) => {
    createCommentMutate({
      comentario: descricao,
      idPublicacao: publicacao.idPublicacao,
      idUsuario: userGlobalData?.idUsuario || "",
    });
  };

  useEffect(() => {
    if (isSuccessCreateComment) {
      reset();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccessCreateComment]);

  useEffect(() => {
    if (isErrorCreateComment) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: "O comentário não deve conter termos ofensivos!",
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isErrorCreateComment]);

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger>{trigger}</DialogTrigger>

      <DialogContent className="flex flex-col justify-between border rounded-lg gap-4 md:w-2/5 w-4/5 border-black-2 p-8 md:p-4 shadow-lg h-2/3 md:h-3/4">
        <div>
          <DialogHeader className="flex flex-col w-full">
            <div className="flex w-full h-max items-center gap-5 pb-4">
              <DialogTitle className="flex flex-row justify-center items-center w-10 h-10">
                <Link
                  href={`/profile/${publicacao.usuario.idUsuario}`}
                  className="h-full flex flex-col !justify-center items-center w-max  lg:w-[50px] lg:h-[50px]"
                >
                  <Image
                    width={100}
                    height={100}
                    className="w-10 h-10 md:w-6 md:h-6 md:ml-5 rounded-3xl"
                    src={publicacao.usuario.urlMidia}
                    alt="Imagem do usuário que fez a postagem."
                  />
                  <DialogDescription className="hidden"></DialogDescription>
                </Link>
              </DialogTitle>
              <div className="flex flex-col gap-y-2 md:py-2 justify-end">
                <DialogDescription className="flex flex-col gap-y-2 md:py-2 justify-end font-bold text-complementary-black">
                  {publicacao.usuario.nome}
                </DialogDescription>
              </div>
            </div>
            <hr className="border-black-2 md:ml-8 w-full md:w-5/6 self-center justify-self-center " />
          </DialogHeader>
          <ScrollArea className="mt-6">
            <div className="flex flex-col justify-start h-full w-full gap-5 md:gap-0">
              {comentarios && comentarios?.length > 0 ? (
                comentarios
                  .sort((a, b) => {
                    if (a.comentario < b.comentario) return -1; // a vem antes de b
                    if (a.comentario > b.comentario) return 1; // b vem antes de a
                    return 0; // são iguais
                  })
                  .map((comentario, index) => (
                    <Comment
                      initial={{ opacity: 0, x: -100 }}
                      animate={{ opacity: 1, x: 0 }}
                      exit={{ opacity: 0, x: -100 }}
                      transition={{ duration: 0.2, delay: 0.1 + index * 0.1 }}
                      comentario={comentario}
                      key={comentario.idComentario}
                    />
                  ))
              ) : (
                <div className="w-full h-full flex flex-col justify-center items-center mt-20 ">
                  <span className="text-xl text-center">
                    Nenhum comentário encontrado.
                  </span>
                  <Image
                    width={350}
                    height={350}
                    src={"/icons/not-found.svg"}
                    alt="Imagem de página não encontrada."
                  />
                </div>
              )}
            </div>
          </ScrollArea>
        </div>
        <div>
          <div className="h-8">
            {/* <FaRegHeart className="z-10 -mb-2 w-5 h-5 md:block hidden" /> */}
            <hr className="border-black-2 w-full md:w-[88.88%] md:ml-8 self-center justify-self-center" />
          </div>
          <form
            onSubmit={handleSubmit(createComment)}
            className="flex flex-row gap-2 justify-between"
          >
            <input
              {...register("descricao")}
              // value={comment}
              // onChange={(e) => setComment(e.target.value)}
              placeholder="Adicione um comentário"
              type="text"
              className="w-full md:mx-8 rounded-3xl px-4 min-h-8 bg-[#EDF3F8] focus:outline-none
            font-montserrat font-regular font-complementary-black text-xs placeholder-complementary-blue"
            />

            <AnimatePresence>
              {comment && (
                <motion.button
                  initial={{ opacity: 0, x: -100 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: -100 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer"
                >
                  <IoSendSharp className="min-h-8 min-w-6" color="#0A66C2" />
                </motion.button>
              )}
            </AnimatePresence>
          </form>
          <Toaster />
        </div>
      </DialogContent>
    </Dialog>
  );
};

type CommentEntry = ComponentProps<typeof motion.div> & {
  comentario: CommentType;
};

const Comment = ({ comentario, ...rest }: CommentEntry) => {
  const { mutate: deleteCommentMutate } = useDeleteComment(
    comentario.publicacao.idPublicacao
  );

  const { userGlobalData } = useContext(AuthContext);

  const handleDeleteComment = () => {
    deleteCommentMutate(comentario.idComentario);
  };

  return (
    <motion.div className="flex flex-row gap-4 md:mx-6" {...rest}>
      <div className="flex justify-center items-center w-10 h-10">
        <Link
          href={`/profile/${comentario.usuario.idUsuario}`}
          className="h-full flex flex-col !justify-center items-center w-max  lg:w-[50px] lg:h-[50px]"
        >
          <Image
            width={100}
            height={100}
            className="min-w-10 w-10 h-10 md:min-w-6 md:w-6 md:h-6 rounded-3xl cursor-pointer"
            src={comentario.usuario.urlMidia}
            alt="Imagem de perfil do usuário que comentou na publicação."
          />
        </Link>
      </div>

      <ContextMenu>
        <ContextMenuTrigger>
          <div className="flex flex-col w-full gap-y-2 md:py-2">
            <p className="align-top text-sm font-medium font-montserrat w-full">
              {comentario.usuario.nome}
            </p>
            <span className="align-bottom text-xs font-medium font-montserrat w-[90%] text-pretty">
              <TooltipProvider>
                <Tooltip>
                  <TooltipTrigger className="text-start w-max">
                    {comentario.comentario.length > 45
                      ? comentario.comentario.substring(0, 45) + "..."
                      : comentario.comentario}
                  </TooltipTrigger>
                  <TooltipContent>
                    <p>{comentario.comentario}</p>
                  </TooltipContent>
                </Tooltip>
              </TooltipProvider>
            </span>
          </div>
        </ContextMenuTrigger>

        {comentario.usuario.idUsuario === userGlobalData?.idUsuario && (
          <ContextMenuContent className="bg-complementary-white !mr-20 p-5 border !rounded-md border-complementary-black">
            <ContextMenuItem
              onClick={handleDeleteComment}
              className="text-[#E71A1A] flex gap-2 hover:cursor-pointer"
            >
              Excluir Comentário
            </ContextMenuItem>
          </ContextMenuContent>
        )}
      </ContextMenu>
    </motion.div>
  );
};

export const FollowersModal = ({
  trigger,
  users,
  userIdSearched,
  isFollow = false,
}: FollowersOrFollowedModalProps) => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <Dialog open={isOpen} onOpenChange={setIsOpen}>
      <DialogTrigger>{trigger}</DialogTrigger>
      <DialogContent className="flex flex-col justify-between border rounded-lg gap-4 md:w-2/5 w-4/5 border-black-2 p-8 md:p-4 shadow-lg h-2/3 md:h-3/4">
        <div>
          <DialogHeader className="flex flex-col w-full border-b-black-2 mb-4">
            <DialogTitle className="text-center font-semibold">
              {isFollow ? "Seguidores" : "Seguindo"}
            </DialogTitle>
            <DialogDescription className="hidden"></DialogDescription>
            <hr className="border-black-2 w-full md:w-5/6 self-center justify-self-center " />
          </DialogHeader>

          <ScrollArea className="mt-10">
            <div className="flex flex-col justify-start h-full w-full gap-5 md:gap-0">
              {users.length > 0 ? (
                users.map((user, index) => (
                  <FollowerEntry
                    initial={{ opacity: 0, x: -200 }}
                    animate={{ opacity: 1, x: 0 }}
                    exit={{ opacity: 0, x: -200 }}
                    transition={{ duration: 0.2, delay: 0.1 + index * 0.1 }}
                    isFollow={isFollow}
                    userIdSearched={userIdSearched}
                    user={user}
                    key={user.idUsuario}
                  />
                ))
              ) : (
                <div className="w-full h-full flex flex-col justify-center items-center mt-20 ">
                  <span className="text-xl text-center">
                    Nenhum usuário encontrado.
                  </span>
                  <Image
                    width={350}
                    height={350}
                    src={"/icons/not-found.svg"}
                    alt="Imagem de página não encontrada."
                  />
                </div>
              )}
            </div>
          </ScrollArea>
        </div>
      </DialogContent>
    </Dialog>
  );
};

type FollowerEntryProps = ComponentProps<typeof motion.div> & {
  user: userAuthType;
  userIdSearched: string;
  isFollow: boolean;
};

const FollowerEntry = ({
  user,
  userIdSearched,
  isFollow,
  ...rest
}: FollowerEntryProps) => {
  const { userGlobalData } = useContext(AuthContext);

  const { mutate: unFollowUserMutate } = useUnFollowUser(userIdSearched);

  const handleUnFollowUser = () => {
    unFollowUserMutate({
      idSeguido: user.idUsuario,
      idSeguidor: userGlobalData?.idUsuario || "",
    });
  };

  const handleRemoveFollowerUser = () => {
    unFollowUserMutate({
      idSeguidor: user.idUsuario,
      idSeguido: userGlobalData?.idUsuario || "",
    });
  };

  return (
    <motion.div {...rest} className="w-full md:h-16 flex flex-row gap-x-1">
      <Link
        href={`/profile/${user.idUsuario}`}
        className="min-w-10 w-10 h-10 min-h-10"
      >
        <Image
          width={60}
          height={60}
          className="min-w-10 min-h-10 rounded-3xl cursor-pointer"
          src={user.urlMidia}
          alt="Imagem de perfil do usuário que comentou na publicação."
        />
      </Link>
      <div className="md:flex items-center w-full h-12 justify-between ml-2">
        <p className="font-semibold text-xs md:text-sm w-full md:w-1/2">
          {user.nome}
        </p>
        {userGlobalData?.idUsuario === userIdSearched && (
          <button
            onClick={isFollow ? handleRemoveFollowerUser : handleUnFollowUser}
            className="items-center h-1/2 rounded-lg hover:bg-[#22222222]"
          >
            <span className="text-xs md:text-md text-complementary-blue font-semibold">
              {isFollow ? "Remover" : "Parar de seguir"}
            </span>
          </button>
        )}
      </div>
    </motion.div>
  );
};
