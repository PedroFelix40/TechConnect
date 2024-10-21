import Api, { commentResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const deleteComment = async (commentId: string) => {
  await Api.delete(`${commentResource}?IdComentario=${commentId}`);
};

export const useDeleteComment = (IdPostagem: string) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: deleteComment,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-comments-by-post", IdPostagem],
      });
    },
  });
};
