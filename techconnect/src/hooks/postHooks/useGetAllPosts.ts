import Api, { postResource } from "@/Service/Api";
import { postType } from "@/Types";
import { useQuery } from "@tanstack/react-query";

const getAllPosts = async () => {
  const { data } = await Api.get<postType[]>(postResource);

  return data;
};

export const useGetAllPosts = () => {
  return useQuery({
    queryKey: ["get-all-posts"],
    queryFn: getAllPosts,
  });
};
