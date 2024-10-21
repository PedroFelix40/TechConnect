import Api, { chatResource } from "@/Service/Api";
import { useMutation } from "@tanstack/react-query";

type usersIdChat = {
  idUsuario1: string;
  idUsuario2: string;
};

const createChat = async (usersIdChat: usersIdChat) => {
  await Api.post(chatResource, usersIdChat);
};

export const useCreateChat = () => {
  return useMutation({
    mutationFn: createChat,
  });
};
