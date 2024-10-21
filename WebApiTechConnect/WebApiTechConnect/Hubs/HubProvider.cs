using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Mensagem;

namespace WebApiTechConnect.Hubs
{
    public class HubProvider : Hub<IHubProvider>
    {

        private readonly TechConnectContext _context;

        public HubProvider()
        {
            _context = new TechConnectContext();
        }


        public async Task JoinChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }


        public async Task SendMessage(CadastrarMensagem cadastrarMensagem)
        {

            ChatMessage chatMessage = new()
            {
                IdChat = cadastrarMensagem.IdChat,
                IdRemetente = cadastrarMensagem.IdRemetente,
                Mensagem = cadastrarMensagem.Mensagem,
            };

            Chat chatBuscado = _context.Chat
                .Include(x => x.Usuario1).ThenInclude(x => x!.Midia)
                .Include(x => x.Usuario2).ThenInclude(x => x!.Midia)
                .FirstOrDefault(x => x.IdUsuario1 == chatMessage.IdRemetente || x.IdUsuario2 == chatMessage.IdRemetente)!;

            ExibirMensagem exibirMensagem = new()
            {
                IdMensagem = chatMessage.IdChatMessage,
                DataHoraEnvio = chatMessage.DataHoraEnvio,
                Mensagem = chatMessage.Mensagem,
                Remetente = new()
                {
                    IdUsuario = chatMessage.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.IdUsuario1 : chatBuscado.IdUsuario2,
                    Nome = chatMessage.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.Usuario1!.Nome : chatBuscado.Usuario2!.Midia!.UrlMidia,
                    UrlMidia = chatMessage.IdRemetente == chatBuscado.IdUsuario1 ? chatBuscado.Usuario1!.Midia!.UrlMidia : chatBuscado.Usuario2!.Midia!.UrlMidia
                },

                Destinatario = new()
                {
                    IdUsuario = chatMessage.IdRemetente != chatBuscado.IdUsuario1 ? chatBuscado.IdUsuario1 : chatBuscado.IdUsuario2,
                    Nome = chatMessage.IdRemetente != chatBuscado.IdUsuario1 ? chatBuscado.Usuario1!.Nome : chatBuscado.Usuario2!.Midia!.UrlMidia,
                    UrlMidia = chatMessage.IdRemetente != chatBuscado.IdUsuario1 ? chatBuscado.Usuario1!.Midia!.UrlMidia : chatBuscado.Usuario2!.Midia!.UrlMidia
                }
            };

            _context.ChatMessage.Add(chatMessage);
            _context.SaveChanges();

            await Clients.Group(cadastrarMensagem.IdChat.ToString()).ReceivedMessage(exibirMensagem);
        }

        // Método para notificar que um usuário está digitando
        public async Task NotifyTyping(Guid chatId, Guid userId, bool isTyping)
        {
            await Clients.OthersInGroup(chatId.ToString()).NotifyTyping(chatId, userId, isTyping);
        }
    }
}
