using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Mensagem;

namespace WebApiTechConnect.Hubs
{
    public interface IHubProvider
    {
        Task ReceivedMessage(ExibirMensagem message);
        Task SendMessage(CadastrarMensagem message);

        // Método para notificar que o usuário está digitando
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chatId">id do chat</param>
        /// <param name="userId">id do usuario que está digitando</param>
        /// <param name="isTyping"></param>
        /// <returns></returns>
        Task NotifyTyping(Guid chatId, Guid userId, bool isTyping);
    }
}