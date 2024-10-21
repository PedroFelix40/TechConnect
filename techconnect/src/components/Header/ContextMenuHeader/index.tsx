"use client";
import {
  ContextMenu,
  ContextMenuContent,
  ContextMenuItem,
  ContextMenuTrigger,
} from "@/components/ui/context-menu";
import { AuthContext } from "@/providers/AuthProvider";
import { useContext } from "react";
import { FaUser } from "react-icons/fa";
import { CiLogout } from "react-icons/ci";
import "./style.css";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/components/ui/tooltip";
import Link from "next/link";
import { useRouter } from "next/navigation";
import Cookie from "js-cookie";
import Image from "next/image";
import { IoChatboxEllipses } from "react-icons/io5";

const ContextMenuHeader = () => {
  const { userGlobalData, setUserGlobalData } = useContext(AuthContext);
  const router = useRouter();

  const handleLogout = () => {
    router.replace("/auth");
    Cookie.remove("user");
    setUserGlobalData(null);
  };

  return (
    <ContextMenu>
      <ContextMenuTrigger>
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger>
              <Image
                className="rounded-xl hover:cursor-pointer w-[50px] h-[50px]"
                src={userGlobalData?.urlMidia || ""}
                width={100}
                height={100}
                alt="Imagem do perfil."
              />
            </TooltipTrigger>
            <TooltipContent className="">
              <span>Clique com o direito</span>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
      </ContextMenuTrigger>
      <ContextMenuContent className="bg-complementary-white">
        <ContextMenuItem>
          <Link
            className=" items-center flex gap-2 hover:cursor-pointer"
            href={`/profile/${userGlobalData?.idUsuario}`}
          >
            <FaUser /> Minha Conta
          </Link>
        </ContextMenuItem>
        <ContextMenuItem>
          <Link
            className=" items-center flex gap-2 hover:cursor-pointer"
            href={`/chat`}
          >
            <IoChatboxEllipses /> Chats
          </Link>
        </ContextMenuItem>
        <ContextMenuItem
          onClick={handleLogout}
          className=" items-center text-[#E71A1A] flex gap-2 hover:cursor-pointer"
        >
          <CiLogout size={18} /> Sair
        </ContextMenuItem>
      </ContextMenuContent>
    </ContextMenu>
  );
};

export default ContextMenuHeader;
