using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Chat;
using WebApiTechConnect.ViewModels.Mensagem;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Repositories
{
    public class ChatRepository : IChat
    {

        private readonly TechConnectContext _context;

        public ChatRepository()
        {
            _context = new TechConnectContext();
        }

        public ExibirChat BuscarChatPorIdDoUsuario(Guid idUsuario1, Guid idUsuario2)
        {
            // Busca o chat específico entre dois usuários
            Chat? chatBuscado = _context.Chat
                .Include(x => x.Usuario1).ThenInclude(x => x!.Midia)
                .Include(x => x.Usuario2).ThenInclude(x => x!.Midia)
                .FirstOrDefault(x =>
                    (x.IdUsuario1 == idUsuario1 && x.IdUsuario2 == idUsuario2) ||
                    (x.IdUsuario1 == idUsuario2 && x.IdUsuario2 == idUsuario1));

            if (chatBuscado == null)
            {
                // Retorna null ou lança uma exceção se o chat não for encontrado
                return null;
            }

            // Busca todas as mensagens associadas a esse chat
            List<ChatMessage> chatMessagesBuscadas = _context.ChatMessage
                .Include(x => x.Remetente).ThenInclude(x => x!.Midia)
                .Where(x => x.IdChat == chatBuscado.IdChat)
                .OrderByDescending(x => x.DataHoraEnvio)
                .ToList();

            // Dicionário para associar mensagens ao chat
            Dictionary<Guid, List<ExibirMensagem>> chatMessagesDict = new();

            // Processa as mensagens e organiza por chat
            foreach (var message in chatMessagesBuscadas)
            {
                // Cria a mensagem formatada
                ExibirMensagem exibirMensagem = new()
                {
                    IdMensagem = message.IdChatMessage,
                    Mensagem = message.Mensagem,
                    DataHoraEnvio = message.DataHoraEnvio,
                    Remetente = new()
                    {
                        IdUsuario = message.Remetente!.IdUsuario,
                        Nome = message.Remetente.Nome,
                        UrlMidia = message.Remetente.Midia!.UrlMidia
                    },
                    Destinatario = new()
                    {
                        IdUsuario = message.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.IdUsuario2 : chatBuscado.IdUsuario1,
                        Nome = message.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.Usuario2!.Nome : chatBuscado.Usuario1!.Nome,
                        UrlMidia = message.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.Usuario2!.Midia!.UrlMidia : chatBuscado.Usuario1!.Midia!.UrlMidia
                    }
                };

                // Adiciona a mensagem ao chat correspondente
                if (!chatMessagesDict.ContainsKey(message.IdChat))
                {
                    chatMessagesDict[message.IdChat] = new List<ExibirMensagem>();
                }
                chatMessagesDict[message.IdChat].Add(exibirMensagem);
            }

            // Cria o chat formatado
            ExibirChat exibirChat = new()
            {
                IdChat = chatBuscado.IdChat,
                Usuario1 = new()
                {
                    IdUsuario = chatBuscado.Usuario1!.IdUsuario,
                    Nome = chatBuscado.Usuario1.Nome,
                    UrlMidia = chatBuscado.Usuario1.Midia!.UrlMidia
                },
                Usuario2 = new()
                {
                    IdUsuario = chatBuscado.Usuario2!.IdUsuario,
                    Nome = chatBuscado.Usuario2.Nome,
                    UrlMidia = chatBuscado.Usuario2.Midia!.UrlMidia
                },
                // Associa as mensagens do chat ou uma lista vazia se não houver mensagens
                Mensagens = chatMessagesDict.ContainsKey(chatBuscado.IdChat) ? chatMessagesDict[chatBuscado.IdChat] : new List<ExibirMensagem>()
            };

            return exibirChat;
        }


        public List<ExibirChat> BuscarChatsDoUsuario(Guid idUsuario)
        {
            // Busca todas as mensagens associadas ao usuário
            List<ChatMessage> chatMessagesBuscadas = _context.ChatMessage
                .Include(x => x.Chat)
                .Include(x => x.Chat!.Usuario1).ThenInclude(x => x!.Midia)
                .Include(x => x.Chat!.Usuario2).ThenInclude(x => x!.Midia)
                .Include(x => x.Remetente).ThenInclude(x => x!.Midia)
                .Where(x => x.IdRemetente == idUsuario || x.Chat!.IdUsuario1 == idUsuario || x.Chat!.IdUsuario2 == idUsuario)
                .OrderByDescending(x => x.DataHoraEnvio).Reverse()
                .ToList();

            // Busca todos os chats associados ao usuário (com ou sem mensagens)
            List<Chat> chatsBuscados = _context.Chat
                .Include(x => x.Usuario1).ThenInclude(x => x!.Midia)
                .Include(x => x.Usuario2).ThenInclude(x => x!.Midia)
                .Where(x => x.IdUsuario1 == idUsuario || x.IdUsuario2 == idUsuario)
                .ToList();

            // Dicionário para associar mensagens aos chats
            Dictionary<Guid, List<ExibirMensagem>> chatMessagesDict = new();

            // Processa as mensagens e organiza por chat
            foreach (var message in chatMessagesBuscadas)
            {
                // Cria a mensagem formatada
                ExibirMensagem exibirMensagem = new()
                {
                    IdMensagem = message.IdChatMessage,
                    Mensagem = message.Mensagem,
                    DataHoraEnvio = message.DataHoraEnvio,
                    Remetente = new()
                    {
                        IdUsuario = message.Remetente!.IdUsuario,
                        Nome = message.Remetente.Nome,
                        UrlMidia = message.Remetente.Midia!.UrlMidia
                    },
                    Destinatario = new()
                    {
                        IdUsuario = message.IdRemetente == message.Chat!.IdUsuario1 ? message.Chat.IdUsuario2 : message.Chat.IdUsuario1,
                        Nome = message.IdRemetente == message.Chat.IdUsuario1 ? message.Chat.Usuario2!.Nome : message.Chat.Usuario1!.Nome,
                        UrlMidia = message.IdRemetente == message.Chat.IdUsuario1 ? message.Chat.Usuario2!.Midia!.UrlMidia : message.Chat.Usuario1!.Midia!.UrlMidia
                    }
                };

                // Adiciona a mensagem ao chat correspondente
                if (!chatMessagesDict.ContainsKey(message.IdChat))
                {
                    chatMessagesDict[message.IdChat] = new List<ExibirMensagem>();
                }
                chatMessagesDict[message.IdChat].Add(exibirMensagem);
            }

            // Lista para armazenar os chats formatados
            List<ExibirChat> chatsFormatados = new();

            // Processa os chats buscados
            foreach (var chat in chatsBuscados)
            {
                // Cria um chat formatado
                ExibirChat exibirChat = new()
                {
                    IdChat = chat.IdChat,
                    Usuario1 = new()
                    {
                        IdUsuario = chat.Usuario1!.IdUsuario,
                        Nome = chat.Usuario1.Nome,
                        UrlMidia = chat.Usuario1.Midia!.UrlMidia
                    },
                    Usuario2 = new()
                    {
                        IdUsuario = chat.Usuario2!.IdUsuario,
                        Nome = chat.Usuario2.Nome,
                        UrlMidia = chat.Usuario2.Midia!.UrlMidia
                    },
                    // Associa as mensagens do chat ou uma lista vazia se não houver mensagens
                    Mensagens = chatMessagesDict.ContainsKey(chat.IdChat) ? chatMessagesDict[chat.IdChat] : new List<ExibirMensagem>()
                };

                // Adiciona o chat à lista final
                chatsFormatados.Add(exibirChat);
            }

            return chatsFormatados;
        }

        public bool CriarChat(Guid idUsuario1, Guid idUsuario2)
        {
            Chat chatBuscado = _context.Chat.FirstOrDefault(x => (x.IdUsuario1 == idUsuario1 && x.IdUsuario2 == idUsuario2) || (x.IdUsuario1 == idUsuario2 && x.IdUsuario2 == idUsuario1))!;

            if (chatBuscado != null || idUsuario1 == idUsuario2)
            {
                return false;
            }

            Chat novoChat = new()
            {
                IdUsuario1 = idUsuario1,
                IdUsuario2 = idUsuario2,
            };


            _context.Chat.Add(novoChat);
            _context.SaveChanges();

            return true;

        }

        public bool ExcluirChat(Guid idChat)
        {
            Chat chatBuscado = _context.Chat.FirstOrDefault(x => x.IdChat == idChat)!;

            if (chatBuscado == null)
            {
                return false;
            }




            _context.Chat.Remove(chatBuscado);
            _context.SaveChanges();

            return true;
        }
    }
}
