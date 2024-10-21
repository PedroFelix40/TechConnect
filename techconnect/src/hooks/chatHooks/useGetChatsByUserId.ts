import Api, { chatResource } from "@/Service/Api";
import { ChatType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getChatsByUserId = async ({ queryKey }: { queryKey: [string] }) => {
  const userId = queryKey[0];
  const { data } = await Api.get<ChatType[]>(
    `${chatResource}?idUsuario=${userId}`
  );

  return data;
};

export const useGetChatsByUserId = (userId: string) => {
  return useQuery({
    queryFn: getChatsByUserId,
    queryKey: [userId],
    enabled: !!userId,
  });
};
