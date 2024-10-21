"use client";
import { ButtonGoogle } from "@/components/button";
import { useCreateUser } from "@/hooks/userHooks/useCreateUser";
import { useLogin } from "@/hooks/userHooks/useLogin";
import Image from "next/image";
import { jwtDecode } from "jwt-decode";
import { userChatType, userPayloadTokenType } from "@/Types";
import { useContext, useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { useUniqueUserData } from "@/hooks/userHooks/useUniqueUserData";
import { AuthContext } from "@/providers/AuthProvider";
import Cookie from "js-cookie";
import { useGoogleLogin } from "@react-oauth/google";
import { useGoogleLoginData } from "@/hooks/userHooks/useGoogleLoginData";
import { useToast } from "@/hooks/use-toast";
import { Toaster } from "@/components/ui/toaster";
import { motion } from "framer-motion";
import Main from "@/components/Main";

const AuthPage = () => {
  const router = useRouter();

  const [accessToken, setAccessToken] = useState("");

  const { data: userGoogle, isLoading: isLoadingUserGoogle } =
    useGoogleLoginData(accessToken);

  const { setUserGlobalData } = useContext(AuthContext);

  const { data: usuarioBuscado, isLoading: isLoadingUsuarioBuscado } =
    useUniqueUserData(
      userGoogle
        ? {
            email: userGoogle.email,
            googleId: userGoogle.sub,
          }
        : {
            email: "",
            googleId: "",
          }
    );

  const {
    mutate: loginMutate,
    data: authTokenApi,
    isError: isErrorLogin,
  } = useLogin();

  const { toast } = useToast();

  const {
    mutate: createUser,
    data: createdUser,
    isError: isErrorCreateUser,
  } = useCreateUser();

  const loginWithGoogle = useGoogleLogin({
    onSuccess: async (response) => {
      setAccessToken(response.access_token);
    },
    onError: () => {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: "Não foi possível realizar login com google!",
      });
    },
    flow: "implicit", // Use implicit, pois 'token' não é suportado
    scope: "openid email profile", // Garantir que o 'profile' esteja no escopo
  });

  //pega os dados do google e cria o ususario ou faz login
  useEffect(() => {
    if (userGoogle) {
      if (!usuarioBuscado) {
        createUser({
          email: userGoogle.email,
          googleId: userGoogle.sub,
          nome: userGoogle.name,
          urlMidia: userGoogle.picture,
        });

        return;
      } else {
        loginMutate({
          email: userGoogle.email,
          googleId: userGoogle.sub,
        });
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userGoogle, usuarioBuscado]);

  //login do usuario recem criado
  useEffect(() => {
    if (createdUser) {
      loginMutate({
        email: createdUser.email,
        googleId: createdUser.googleId,
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [createdUser]);

  //decodifica o token e redireciona o usuario para a home]
  useEffect(() => {
    if (authTokenApi) {
      const {
        email,
        jti: idUsuario,
        name: nome,
        urlMidia,
      } = jwtDecode<userPayloadTokenType>(authTokenApi);

      const user: userChatType = {
        token: authTokenApi,
        email,
        idUsuario,
        nome,
        urlMidia,
      };

      setUserGlobalData(user);

      Cookie.set("user", JSON.stringify(user), { expires: 7 });
      router.push("/");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [authTokenApi]);

  useEffect(() => {
    if (isErrorCreateUser && usuarioBuscado) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: "Não foi possível criar o usuário!",
      });
    }
    if (isErrorLogin) {
      toast({
        variant: "destructive",
        title: "Erro!",
        description: "Não foi possível fazer login na aplicação.",
      });
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isErrorLogin, isErrorCreateUser]);

  return (
    <Main isAuthPage>
      <section className="w-screen h-screen flex">
        <div className="hidden md:flex md:justify-center md:items-center md:bg-blueGradient h-full w-1/2 md:px-10 lg:px-20 lg:py-36">
          <motion.div
            animate={{
              y: [0, -50, 0], // Flutua para cima e depois retorna
            }}
            transition={{
              duration: 3, // Duração total da animação
              ease: "easeInOut",
              repeat: Infinity, // Repete infinitamente
              repeatType: "loop",
            }}
          >
            <Image
              width={500}
              height={500}
              className="w-full h-auto"
              src={
                "https://blobvitalhubfilipegoisg2.blob.core.windows.net/containervitalhubfilipegoisg2/auth.png"
              }
              alt="Ícone de mulher segurando coração."
              priority={true}
            />
          </motion.div>
        </div>
        <div className="h-full w-full flex flex-col items-center justify-evenly bg-blueGradient md:w-1/2 md:bg-none">
          <Image
            width={500}
            height={500}
            src={
              "https://blobvitalhubfilipegoisg2.blob.core.windows.net/containervitalhubfilipegoisg2/light-logo.png"
            } //colocar light dps
            alt="Logo da aplicação."
            className="md:hidden w-1/2 h-auto"
            priority
          />
          <Image
            width={500}
            height={500}
            src={
              "https://blobvitalhubfilipegoisg2.blob.core.windows.net/containervitalhubfilipegoisg2/dark-logo.png"
            }
            alt="Logo da aplicação."
            className="hidden md:flex w-1/2 lg:w-1/3"
          />
          <ButtonGoogle
            isLoading={isLoadingUserGoogle || isLoadingUsuarioBuscado}
            onClick={() => loginWithGoogle()}
          />
        </div>
        <Toaster />
      </section>
    </Main>
  );
};

export default AuthPage;
