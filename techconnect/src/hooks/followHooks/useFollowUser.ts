import Api, { followerResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

// Função para seguir um usuário
const followUser = async ({
  followerId,
  followedId,
}: {
  followerId: string;
  followedId: string;
}) => {
  await Api.post(
    `${followerResource}?idSeguidor=${followerId}&idSeguido=${followedId}`
  );
};

// Custom hook para seguir um usuário e invalidar as queries relacionadas
export const useFollowUser = (userId: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: followUser,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["getFollowedsById", userId],
      });
      queryClient.invalidateQueries({
        queryKey: ["getFollowersById", userId],
      });
    },
  });
};
