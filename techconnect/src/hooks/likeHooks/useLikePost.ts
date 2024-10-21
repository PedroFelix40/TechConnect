import Api, { likeResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

type likeProps = {
  postId: string;
  userId: string;
};

const likePost = async ({ postId, userId }: likeProps) => {
  await Api.post(likeResource, {
    idPublicacao: postId,
    idUsuario: userId,
  });
};

export const useLikePost = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: likePost,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-likes-by-post-id-and-user-id"],
      });
      queryClient.invalidateQueries({
        queryKey: ["get-likes-by-post-id"],
      });
    },
  });
};
