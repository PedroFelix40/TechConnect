import Api, { chatResource } from "@/Service/Api";
import { useMutation } from "@tanstack/react-query";

const deleteChat = async (chatId: string) => {
  await Api.delete(`/${chatResource}?idChat=${chatId}`);
};

export const useDeleteChat = () => {
  return useMutation({
    mutationFn: deleteChat,
  });
};
