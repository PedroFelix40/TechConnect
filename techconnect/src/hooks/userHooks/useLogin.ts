import Api, { loginResource } from "@/Service/Api";
import { loginParams } from "@/Types";
import { useMutation } from "@tanstack/react-query";

type loginResponse = {
  token: string;
};

const login = async ({ email, googleId }: loginParams) => {
  const { data } = await Api.post<loginResponse>(loginResource, {
    email,
    googleIdAccount: googleId,
  });

  return data.token;
};

export const useLogin = () => {
  return useMutation({
    mutationFn: login,
  });
};
