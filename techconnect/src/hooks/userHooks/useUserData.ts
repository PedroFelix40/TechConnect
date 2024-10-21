import Api, { userResource } from "@/Service/Api";
import { userType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getAllUsers = async () => {
  const { data } = await Api.get<userType[]>(userResource);

  return data;
};

export const useUserData = () => {
  return useQuery({
    queryFn: getAllUsers,
    queryKey: ["all-users-data"],
  });
};
