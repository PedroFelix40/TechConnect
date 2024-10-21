using WebApiTechConnect.ViewModels.Chat;

namespace WebApiTechConnect.Interfaces
{
    public interface IChat
    {
        bool CriarChat(Guid idUsuario1, Guid idUsuario2);

        bool ExcluirChat(Guid idChat);
        List<ExibirChat> BuscarChatsDoUsuario(Guid idUsuario);
        ExibirChat BuscarChatPorIdDoUsuario(Guid idUsuario1, Guid idUsuario2);
    }
}
