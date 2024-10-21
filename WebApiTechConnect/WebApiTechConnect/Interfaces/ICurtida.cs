using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Curtida;

namespace WebApiTechConnect.Interfaces
{
    public interface ICurtida
    {
        public bool CurtirPublicacao(CadastrarCurtida curtida);
        public bool DescurtirPublicacao(Guid IdCurtida);

        /// <summary>
        /// Busca curtidas através do id do usuario e da publicacao
        /// </summary>
        /// <param name="IdPubublicacao"></param>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public List<ExibirCurtida> BuscarCurtidasDaPublicacaoPorIds(Guid IdPublicacao, Guid IdUsuario);
        public List<ExibirCurtida> BuscarCurtidasDaPublicacaoPorIdPublicacao(Guid IdPublicacao);
    }
}
