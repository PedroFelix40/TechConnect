"use client";
import ChatConversation from "@/components/ChatConversation";
import ChatsList from "@/components/ChatsList";
import Header from "@/components/Header";
import { useGetChatsByUserId } from "@/hooks/chatHooks/useGetChatsByUserId";
import { ChatType, Mensagem } from "@/Types";
import { useContext, useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import Main from "@/components/Main";
import { AuthContext } from "@/providers/AuthProvider";

const ChatPage = () => {
  const { userGlobalData } = useContext(AuthContext);

  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );

  const [selectedChat, setSelectedChat] = useState<ChatType | null>(null);

  const [messages, setMessages] = useState<Mensagem[]>([]);

  const { data: chats } = useGetChatsByUserId(userGlobalData?.idUsuario || "");

  const handleSendMessage = async (message: string) => {
    if (connection && selectedChat) {
      const cadastrarMensagem = {
        mensagem: message,
        idChat: selectedChat.idChat,
        idRemetente: userGlobalData?.idUsuario || "",
      };

      try {
        // Enviar a mensagem para o SignalR hub
        await connection.invoke("SendMessage", cadastrarMensagem);
        console.log("Mensagem enviada:", message);
      } catch (err) {
        console.error("Erro ao enviar a mensagem:", err);
      }
    }
  };

  // Função para inicializar a conexão SignalR
  const startConnection = async () => {
    if (connection) {
      try {
        await connection.start();
        console.log("SignalR Connected.");
      } catch (err) {
        console.log("Error connecting to SignalR:", err);
      }
    }
  };

  // Estabelece a conexão com o SignalR quando o componente for montado
  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl(`https://localhost:7232/Hub`, {
        // Se houver autenticação, você pode passar tokens aqui.
      })
      .withAutomaticReconnect() // Reconeção automática em caso de falha
      .build();

    setConnection(newConnection);
  }, []);

  // Inicia a conexão com o SignalR assim que ela for criada
  useEffect(() => {
    if (connection) {
      startConnection();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [connection]);

  useEffect(() => {
    if (connection && selectedChat) {
      // Conectar ao grupo do chat correspondente
      connection
        .invoke("JoinChat", selectedChat.idChat)
        .then(() => console.log("Joined chat:", selectedChat.idChat))
        .catch((err) => console.error("Error joining chat:", err));
    }
  }, [connection, selectedChat]);

  // Configura o listener para receber mensagens em tempo real
  useEffect(() => {
    if (connection) {
      connection.on("ReceivedMessage", (message: Mensagem) => {
        console.log("Mensagem recebida:", message);
        // Atualiza o estado das mensagens
        setMessages((prevMessages) => [...prevMessages, message]);
      });
      return () => {
        connection.off("ReceivedMessage"); // Limpa o listener ao desmontar
      };
    }
  }, [connection]);

  return (
    <>
      <Header />
      <Main>
        <section className="w-full h-full flex">
          <ChatsList
            setMessages={setMessages}
            selectedChat={selectedChat}
            setSelectedChat={setSelectedChat}
            chats={chats && chats.length ? chats : []}
            currentUserId={userGlobalData?.idUsuario || ""}
          />
          {selectedChat ? (
            <ChatConversation
              handleSubmitMessage={(message) => handleSendMessage(message)}
              currentUserId={userGlobalData?.idUsuario || ""}
              messages={
                selectedChat ? [...selectedChat.mensagens, ...messages] : []
              }
            />
          ) : null}
        </section>
      </Main>
    </>
  );
};

export default ChatPage;
