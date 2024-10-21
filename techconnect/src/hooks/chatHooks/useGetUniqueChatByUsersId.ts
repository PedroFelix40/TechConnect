import Api, { chatResource } from "@/Service/Api";
import { ChatType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

type params = {
  userId1: string;
  userId2: string;
};

const getUniqueChatByUsersId = async ({
  queryKey,
}: {
  queryKey: [string, params];
}) => {
  const { userId1, userId2 } = queryKey[1];

  const { data } = await Api.get<ChatType>(
    `${chatResource}/BuscarChatPorIdDoUsuario?idUsuario1=${userId1}&idUsuario2=${userId2}`
  );
  return data;
};

export const useGetUniqueChatByUsersId = (params: params) => {
  return useQuery({
    queryKey: ["getUniqueChatByUsersId", params],
    queryFn: getUniqueChatByUsersId,
    enabled: !!params,
  });
};
