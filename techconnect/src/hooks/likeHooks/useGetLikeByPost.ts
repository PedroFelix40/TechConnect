import Api, { likeResource } from "@/Service/Api";
import { LikeType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getLikeByPost = async ({ queryKey }: { queryKey: string[] }) => {
  const postId = queryKey[1];

  const { data } = await Api.get<LikeType[]>(
    `${likeResource}/BuscarCurtidasDaPublicacaoPorIdPublicacao?IdPublicacao=${postId}`
  );

  return data;
};

export const useGetLikeByPost = (IdPostagem: string) => {
  return useQuery({
    queryKey: ["get-likes-by-post-id", IdPostagem],
    queryFn: getLikeByPost,
    enabled: !!IdPostagem,
    retry: false,
  });
};
