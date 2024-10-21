import Api, { postResource } from "@/Service/Api";
import { postType } from "@/Types";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const updatePost = async (post: postType) => {
  await Api.put(postResource, post);
};

export const useUpdatePost = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: updatePost,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["get-all-posts"],
      });
    },
  });
};
