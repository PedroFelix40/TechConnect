import Api, { postResource } from "@/Service/Api";
import { postParams } from "@/Types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const createPost = async (post: postParams) => {
  const formData = new FormData();

  formData.append("Arquivo", post.file);
  formData.append("Descricao", post.Descricao);
  formData.append("IdUsuario", post.IdUsuario);

  await Api.post(postResource, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const useCreatePost = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createPost,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-all-posts"],
      });
    },
  });
};
