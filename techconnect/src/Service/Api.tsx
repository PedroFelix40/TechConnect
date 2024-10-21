import axios from "axios";

const apiPort = `:7232`;
const urlApi = `https://localhost${apiPort}/api`;

//Rotas
const userResource = `/Usuario`;
const postResource = `/Publicacao`;
const loginResource = `/Login`;
const likeResource = `/Curtida`;
const commentResource = `/Comentario`;
const followerResource = `/Seguidores`;
const chatResource = `/Chat`;

const Api = axios.create({ baseURL: urlApi });

export default Api;
export {
  userResource,
  loginResource,
  commentResource,
  postResource,
  likeResource,
  followerResource,
  chatResource,
};
