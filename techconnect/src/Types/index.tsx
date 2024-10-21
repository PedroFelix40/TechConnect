import { z } from "zod";

//tipagem tech connect
export type postType = {
  idPublicacao: string;
  descricao: string;
  urlMidia: string;
  dataPublicacao: Date;
  usuario: userType;
};

export type userType = {
  idUsuario: string;
  nome: string;
  email: string;
  urlMidia: string;
};

export type userAuthType = {
  idUsuario: string;
  nome: string;
  email: string;
  googleId: string;
  urlMidia: string;
};

export type userChatType = {
  idUsuario: string;
  nome: string;
  email: string;
  urlMidia: string;
  token: string;
};

export type userPayloadTokenType = {
  email: string;
  name: string;
  jti: string;
  urlMidia: string;
  exp: number;
  iss: string;
  aud: string;
};

export type CommentType = {
  idComentario: string;
  comentario: string;
  dataPublicacao: string;
  usuario: userType;
  publicacao: postType;
};

export type LikeType = {
  idCurtida: string;
  publicacao: postType;
  usuario: userType;
};

export type userChat = {
  idUsuario: string;
  nome: string;
  urlMidia: string;
};

export type Mensagem = {
  idMensagem: string;
  dataHoraEnvio: string;
  mensagem: string;
  remetente: userChat;
  destinatario: userChat;
};

export type ChatType = {
  idChat: string;
  usuario1: userChat;
  usuario2: userChat;
  mensagens: Mensagem[];
};

export interface newsType {
  status: string;
  totalResults: number;
  articles: Article[];
}

export interface Article {
  source: Source;
  author: string;
  title: string;
  description: string;
  url: string;
  urlToImage: string;
  publishedAt: Date;
  content: string;
}

export interface Source {
  id: string;
  name: string;
}

export type GoogleJwtPayload = {
  iss: string;
  azp: string;
  aud: string;
  sub: string;
  email: string;
  email_verified: boolean;
  exp: number;
  family_name: string;
  given_name: string;
  iat: number;
  jti: string;
  name: string;
  nbf: number;
  picture: string;
};

export type loginParams = {
  email: string;
  googleId: string;
};

export type postParams = {
  file: File;
  Descricao: string;
  IdUsuario: string;
};

export const schemaPost = z.object({
  file: z
    .instanceof(FileList)
    .refine((files) => files.length > 0, "O arquivo é obrigatório!")
    .refine(
      (files) => files[0]?.size <= 5 * 1024 * 1024, // 5MB
      "O arquivo deve ter no máximo 5MB!"
    ),
  descricao: z.string().min(1, "Insira uma descrição!"),
});

export type PostFormData = z.infer<typeof schemaPost>;

export const schemaComment = z.object({
  descricao: z.string().min(1, "O comentário é obrigatório!"),
});

export type CommentFormData = z.infer<typeof schemaComment>;

export const schemaGetUser = z.object({
  userName: z.string().min(1),
});

export type GetUserFormData = z.infer<typeof schemaGetUser>;
