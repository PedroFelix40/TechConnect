import Api, { likeResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const dislikePost = async (IdCurtida: string) => {
  await Api.delete(`${likeResource}?IdCurtida=${IdCurtida}`);
};

export const useDislikePost = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: dislikePost,
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
