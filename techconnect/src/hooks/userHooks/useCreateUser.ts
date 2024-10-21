import Api, { userResource } from "@/Service/Api";
import { userAuthType } from "@/Types";
import { useMutation } from "@tanstack/react-query";

type userParams = Omit<userAuthType, "idUsuario"> & {};

const createUser = async (user: userParams) => {
  const { data } = await Api.post<userAuthType>(userResource, user);

  return data;
};

export const useCreateUser = () => {
  return useMutation({
    mutationFn: createUser,
  });
};
