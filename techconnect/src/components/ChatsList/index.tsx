import { ChatType, Mensagem } from "@/Types";
import Image from "next/image";
import { Dispatch, SetStateAction } from "react";
import { ScrollArea } from "../ui/scroll-area";
import { motion } from "framer-motion";

type ChatsListProps = {
  chats: ChatType[];
  currentUserId: string;
  setSelectedChat: Dispatch<SetStateAction<ChatType | null>>;
  selectedChat: ChatType | null;
  setMessages: Dispatch<SetStateAction<Mensagem[]>>;
};

const ChatsList = ({
  chats,
  currentUserId,
  setSelectedChat,
  selectedChat,
  setMessages,
}: ChatsListProps) => {
  const selectChat = (chat: ChatType) => {
    setSelectedChat(chat);
  };

  const handleSelectChat = (chat: ChatType) => {
    selectChat(chat);
    setMessages([]);
  };

  return (
    <aside className={`w-1/3 md:w-1/4 h-full flex flex-col gap-5 border-r-2 `}>
      <ScrollArea>
        {chats &&
          chats.map((chat, index) => {
            return (
              <motion.div
                initial={{ opacity: 0, x: -500 }}
                animate={{ opacity: 1, x: 0 }}
                exit={{ opacity: 0, x: -500 }}
                transition={{ duration: 0.5, delay: 0.1 + index * 0.1 }}
                className={`w-full h-full p-4 md:p-6 hover:scale-y-105 ease-in-out duration-300 ${
                  selectedChat?.idChat === chat.idChat
                    ? "border-r-complementary-blue border-r-2"
                    : ""
                }`}
                key={chat.idChat}
              >
                <button
                  onClick={() => handleSelectChat(chat)}
                  className={`flex items-center w-full h-full gap-3`}
                >
                  <Image
                    width={500}
                    height={500}
                    className="w-full h-full rounded-xl md:w-2/4 2xl:w-1/4"
                    alt="Imagem de perfil do usuário."
                    src={
                      chat.usuario1.idUsuario === currentUserId
                        ? chat.usuario2.urlMidia
                        : chat.usuario1.urlMidia
                    }
                  />

                  <span className="hidden md:flex">
                    {chat.usuario1.idUsuario === currentUserId
                      ? chat.usuario2.nome
                      : chat.usuario1.nome}
                  </span>
                </button>
              </motion.div>
            );
          })}
      </ScrollArea>
    </aside>
  );
};

export default ChatsList;
