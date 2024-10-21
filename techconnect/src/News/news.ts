import moment from "moment";

const currentDate = moment(new Date())
  .subtract(1, "months")
  .format("YYYY-MM-DD");

const language = `pt`;

const apiKey = process.env.NEXT_PUBLIC_API_KEY_NEWS;

export const newsApiRoute = `https://newsapi.org/v2/everything?q=tesla&from=${currentDate}&sortBy=publishedAt&language=${language}&apiKey=${apiKey}`;
