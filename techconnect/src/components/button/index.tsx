"use client";
import { tv } from "tailwind-variants";
import { FaGoogle } from "react-icons/fa";
import { ComponentProps, FC, ReactNode } from "react";
import { BeatLoader } from "react-spinners";
import { MdAdd } from "react-icons/md";

type ButtonProps = ComponentProps<"button"> & {
  isLoading?: boolean;
  children?: ReactNode;
};

type ButtonFollowProps = ComponentProps<"button"> & {
  isFollowing: boolean;
};

// type ButtonGoogleProps = GoogleLoginProps & {};

// Estilização base do botão usando tailwind-variants
const ButtonStyle = tv({
  base: `max-w-[220px] transition-all duration-500 ease-in-out hover:scale-110 text-center text-sm bg-complementary-blue px-10 py-2 text-complementary-white rounded-xl font-montserratAlternates font-bold lg:w-full lg:max-w-full`,
});

// Componente Button com tipagem do TypeScript
const Button: FC<ButtonProps> = ({ children, className = "", ...rest }) => {
  return (
    <button className={ButtonStyle({ className })} {...rest}>
      {children}
    </button>
  );
};

export const ProfileButton = (
  { label = "", number = 0 },
  { isLoading }: ButtonProps
) => {
  return (
    <span className="flex flex-row items-center justify-center w-full h- rounded-lg hover:bg-[#22222222]">
      <p className="text-md py-1 text-complementary-black font-semibold">
        {label}
      </p>
      <p className="ml-1 h-full font-medium text-sm align-middle">{number}</p>
    </span>
  );
};

export const FollowButton = ({ isFollowing, ...rest }: ButtonFollowProps) => {
  return (
    <button
      className={`group flex flex-row items-center justify-center w-full h- rounded-lg hover:bg-[#22222222] ${
        isFollowing ? "bg-slate-500" : ""
      } bg-complementary-blue`}
      {...rest}
    >
      {!isFollowing && (
        <MdAdd className="text-white group-hover:text-complementary-blue" />
      )}
      <p className="text-md py-1 text-white font-semibold group-hover:text-complementary-blue ">
        {!isFollowing ? "Seguir" : "Parar de Seguir"}
      </p>
    </button>
  );
};

export const MessageButton = ({ ...rest }: ComponentProps<"button">) => {
  return (
    <button className="bg-complementary-blue rounded-lg" {...rest}>
      <p className="text-md py-1 text-white font-semibold group-hover:text-complementary-blue ">
        Enviar mensagem
      </p>
    </button>
  );
};

export const ButtonGoogle = ({ isLoading, ...rest }: ButtonProps) => {
  const currentColorSpinner = window.innerWidth > 768 ? "#fff" : "#0A66C2";

  return (
    <button
      disabled={isLoading}
      className="w-3/4 h-[44px] flex items-center justify-center gap-5 bg-white text-complementary-blue font-bold md:bg-complementary-blue md:w-1/2 rounded-md border border-complementary-blue md:text-complementary-white transition-all ease-in-out duration-300 hover:scale-110 cursor-pointer"
      {...rest}
    >
      {isLoading ? (
        <BeatLoader color={currentColorSpinner} size={8} />
      ) : (
        <>
          <FaGoogle size={15} /> Entrar com Google
        </>
      )}
    </button>
    // <GoogleLogin  {...rest} />
  );
};

export default Button;
