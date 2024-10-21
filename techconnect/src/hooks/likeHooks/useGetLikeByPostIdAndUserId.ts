import Api, { likeResource } from "@/Service/Api";
import { postType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

type postParams = {
  postId: string;
  userId: string;
};

const getLikeByPostIdAndUserId = async ({
  queryKey,
}: {
  queryKey: [string, postParams]; // Tipando para receber postId e userId
}) => {
  const { postId, userId } = queryKey[1];

  const { data } = await Api.get<postType[]>(
    `${likeResource}?IdPublicacao=${postId}&IdUsuario=${userId}`
  );

  return data;
};

export const useGetLikeByPostIdAndUserId = (postParams: postParams) => {
  return useQuery({
    queryKey: ["get-likes-by-post-id-and-user-id", postParams],
    queryFn: getLikeByPostIdAndUserId,
    enabled: !!postParams,
    retry: false,
  });
};
