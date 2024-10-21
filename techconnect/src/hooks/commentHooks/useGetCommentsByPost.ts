import Api, { commentResource } from "@/Service/Api";
import { CommentType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getCommentsByPost = async ({ queryKey }: { queryKey: string[] }) => {
  const postId = queryKey[1];

  const { data } = await Api.get<CommentType[]>(
    `${commentResource}?idPublicacao=${postId}`
  );

  return data;
};
export const useGetCommentsByPost = (postId: string) => {
  return useQuery({
    queryKey: ["get-comments-by-post", postId],
    queryFn: getCommentsByPost,
    enabled: !!postId,
    retry: false,
  });
};
