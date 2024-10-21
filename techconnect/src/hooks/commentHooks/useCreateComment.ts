import Api, { commentResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

type commentParams = {
  comentario: string;
  idPublicacao: string;
  idUsuario: string;
};

const createComment = async (comment: commentParams) => {
  await Api.post(commentResource, comment);
};

export const useCreateComment = (IdPostagem: string) => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: createComment,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-comments-by-post", IdPostagem],
      });
    },
  });
};
