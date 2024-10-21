import Api, { followerResource } from "@/Service/Api";
import { userAuthType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getFollowedsById = async ({ queryKey }: { queryKey: string[] }) => {
  const userId = queryKey[1];
  const { data } = await Api.get<userAuthType[]>(
    `${followerResource}/BuscarSeguindo?idUsuario=${userId}`
  );

  return data;
};

export const useGetFollowedsById = (userId: string) => {
  return useQuery({
    queryKey: ["getFollowedsById", userId],
    queryFn: getFollowedsById,
    enabled: !!userId,
  });
};
