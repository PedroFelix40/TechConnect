import Api, { userResource } from "@/Service/Api";
import { userType } from "@/Types";
import { useQuery } from "@tanstack/react-query";
import { loginParams } from "@/Types";

const getUser = async ({ queryKey }: { queryKey: [string, loginParams] }) => {
  const { email, googleId } = queryKey[1];

  const { data } = await Api.get<userType>(
    `${userResource}/BuscarUsuarioPorEmailEGoogleId?Email=${email}&GoogleId=${googleId}`
  );

  return data;
};

export const useUniqueUserData = (loginParams: loginParams) => {
  return useQuery({
    queryFn: getUser,
    queryKey: ["user-unique-data", loginParams],
    enabled: !!loginParams,
    retry: false,
  });
};
