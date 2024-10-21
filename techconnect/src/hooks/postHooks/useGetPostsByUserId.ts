import Api, { postResource } from "@/Service/Api";
import { postType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getPostsByUserId = async ({ queryKey }: { queryKey: string[] }) => {
  const userId = queryKey[1];

  const { data } = await Api.get<postType[]>(
    `${postResource}/ListarPublicacoesPorIdDoUsuario?idUsuario=${userId}`
  );

  return data;
};

export const useGetPostsByUserId = (userId: string) => {
  return useQuery({
    queryKey: ["get-posts-by-User-id", userId],
    queryFn: getPostsByUserId,
    enabled: !!userId,
  });
};
