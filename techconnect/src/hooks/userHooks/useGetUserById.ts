import Api, { userResource } from "@/Service/Api";
import { userType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getUser = async ({ queryKey }: { queryKey: [string] }) => {
  const userId = queryKey[0];

  const { data } = await Api.get<userType>(
    `${userResource}/BuscarPorId?idUsuario=${userId}`
  );

  return data;
};

export const useGetUserById = (userId: string) => {
  return useQuery({
    queryFn: getUser,
    queryKey: [userId],
    enabled: !!userId,
  });
};
