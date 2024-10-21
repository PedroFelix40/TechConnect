using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Interfaces
{
    public interface ISeguidores
    {
        bool Seguir(Guid idSeguidor, Guid idSeguido);

        bool PararDeSeguir(Guid idSeguidor, Guid idSeguido);

        /// <summary>
        /// Busca os seguidores de um usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        List<ExibirUsuario> BuscarSeguidores(Guid idUsuario);

        /// <summary>
        /// Busca todos os usuarios que a pessoa está seguindo
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        List<ExibirUsuario> BuscarSeguindo(Guid idUsuario);
    }
}
