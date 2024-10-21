import Api, { postResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const deletePost = async (postId: string) => {
  await Api.delete(`${postResource}?idPublicacao=${postId}`);
};

export const useDeletePost = (userId?: string) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: deletePost,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-all-posts"],
      });

      if (userId) {
        queryClient.invalidateQueries({
          queryKey: ["get-posts-by-User-id", userId],
        });
      }
    },
  });
};
