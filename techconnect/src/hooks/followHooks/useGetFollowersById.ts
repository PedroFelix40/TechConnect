import Api, { followerResource } from "@/Service/Api";
import { userAuthType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getFollowersById = async ({ queryKey }: { queryKey: string[] }) => {
  const userId = queryKey[1];
  const { data } = await Api.get<userAuthType[]>(
    `${followerResource}/BuscarSeguidores?idUsuario=${userId}`
  );

  return data;
};

export const useGetFollowersById = (userId: string) => {
  return useQuery({
    queryKey: ["getFollowersById", userId],
    queryFn: getFollowersById,
    enabled: !!userId,
  });
};
