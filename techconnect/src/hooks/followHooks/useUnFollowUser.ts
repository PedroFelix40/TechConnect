import Api, { followerResource } from "@/Service/Api";
import { useMutation, useQueryClient } from "@tanstack/react-query";

const unFollowUser = async ({
  idSeguidor,
  idSeguido,
}: {
  idSeguidor: string;
  idSeguido: string;
}) => {
  await Api.delete(
    `${followerResource}?idSeguidor=${idSeguidor}&idSeguido=${idSeguido}`
  );
};

export const useUnFollowUser = (userId: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: unFollowUser,
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
