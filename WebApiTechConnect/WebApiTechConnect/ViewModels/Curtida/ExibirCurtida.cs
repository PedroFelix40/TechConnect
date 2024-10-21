using WebApiTechConnect.ViewModels.Publicacao;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.ViewModels.Curtida
{
    public class ExibirCurtida
    {
        public Guid IdCurtida { get; set; }

        public ExibirPublicacao? Publicacao { get; set; }

        public ExibirUsuario? Usuario { get; set; }
    }
}
