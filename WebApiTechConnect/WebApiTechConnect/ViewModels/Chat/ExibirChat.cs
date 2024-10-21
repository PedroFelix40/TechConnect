using WebApiTechConnect.ViewModels.Mensagem;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.ViewModels.Chat
{
    public class ExibirChat
    {
        public Guid IdChat { get; set; }
        public ExibirUsuarioMensagem? Usuario1 { get; set; }
        public ExibirUsuarioMensagem? Usuario2 { get; set; }
        public List<ExibirMensagem>? Mensagens { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is ExibirChat chat)
            {
                return this.IdChat == chat.IdChat;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.IdChat.GetHashCode();
        }
    }
}
