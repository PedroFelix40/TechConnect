import { newsApiRoute } from "@/News/news";
import { newsType } from "@/Types";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";

interface NewsArticle {
  url: string;
  title: string;
}

const getRandomNews = (arr: NewsArticle[], numItems: number): NewsArticle[] => {
  const shuffled = arr.sort(() => 0.5 - Math.random());
  return shuffled.slice(0, numItems);
};

const getNews = async () => {
  const { data } = await axios.get<newsType>(newsApiRoute);

  const randomNews = getRandomNews(
    data.articles.filter(
      (element) => element.title !== "[Removed]" && element.title
    ),
    3
  );

  return randomNews;
};

export const useNews = () => {
  return useQuery({
    queryKey: ["get-news"],
    queryFn: getNews,
  });
};
