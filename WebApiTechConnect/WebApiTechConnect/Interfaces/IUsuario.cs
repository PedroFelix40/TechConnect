using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Interfaces
{
    public interface IUsuario
    {
        ExibirUsuario BuscarUsuarioPorEmailEGoogleId(string Email, string GoogleId);
        ExibirUsuario Cadastrar(CadastrarUsuario usuario);
        List<ExibirUsuario> ListarUsuarios();
        ExibirUsuario BuscarUsuarioPorId(Guid IdUsuario);

    }
}
