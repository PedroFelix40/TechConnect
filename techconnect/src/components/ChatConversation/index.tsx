import {
  MinChatUiProvider,
  MainContainer,
  MessageInput,
  MessageContainer,
  MessageList,
  //   MessageHeader,
} from "@minchat/react-chat-ui";
import { Mensagem } from "@/Types";

type ChatConversationProps = {
  messages: Mensagem[];
  currentUserId: string;
  handleSubmitMessage: (e: string) => void;
};

const ChatConversation = ({
  messages,
  currentUserId,
  handleSubmitMessage,
}: ChatConversationProps) => {
  const formatedMessages = messages.map((message) => ({
    text: message.mensagem,
    user: {
      id: message.remetente.idUsuario,
      name: message.remetente.nome,
      avatar: message.remetente.urlMidia,
    },

    id: message.idMensagem,
  }));

  const MainContainerStyle = {
    height: window.innerHeight - 80,
    width: "100%",
    backgroundColor: "#F3F4F6",
    display: "flex",
    justifyContent: "center",
  };

  const handleTyping = () => {};

  return (
    <MinChatUiProvider theme="#0A66C2">
      <MainContainer style={MainContainerStyle}>
        <MessageContainer>
          {/* <MessageHeader /> */}
          <MessageList
            currentUserId={currentUserId}
            messages={formatedMessages}
          />
          <MessageInput
            onSendMessage={(message: string) => handleSubmitMessage(message)}
            onStartTyping={handleTyping}
            showSendButton
            placeholder="Escreva aqui"
          />
        </MessageContainer>
      </MainContainer>
    </MinChatUiProvider>
  );
};

export default ChatConversation;
