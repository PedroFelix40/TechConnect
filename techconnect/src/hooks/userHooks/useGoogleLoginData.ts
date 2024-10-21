import { useQuery } from "@tanstack/react-query";
import axios from "axios";

type userGoogleResponse = {
  sub: string;
  name: string;
  given_name: string;
  family_name: string;
  picture: string;
  email: string;
  email_verified: boolean;
};

const getUserGoogle = async ({ queryKey }: { queryKey: [string] }) => {
  const accessToken = queryKey[0];

  const { data } = await axios.get<userGoogleResponse>(
    "https://www.googleapis.com/oauth2/v3/userinfo",
    {
      headers: {
        Authorization: `Bearer ${accessToken}`, // Envia o token no cabeçalho da requisição
      },
    }
  );

  return data;
};

export const useGoogleLoginData = (accessToken: string) => {
  return useQuery({
    queryKey: [accessToken],
    queryFn: getUserGoogle,
    enabled: !!accessToken,
  });
};
