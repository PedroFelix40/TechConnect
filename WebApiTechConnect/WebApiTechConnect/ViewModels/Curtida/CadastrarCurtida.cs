using WebApiTechConnect.ViewModels.Publicacao;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.ViewModels.Curtida
{
    public class CadastrarCurtida
    {
        public Guid IdPublicacao { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
