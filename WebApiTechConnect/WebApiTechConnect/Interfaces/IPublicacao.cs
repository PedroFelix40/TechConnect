using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Publicacao;

namespace WebApiTechConnect.Interfaces
{
    public interface IPublicacao
    {
        public Task CadastrarPublicacao(CadastrarPublicacao publicacao);

        public bool EditarPublicacao(AtualizarPublicacao publicacao);
        public List<ExibirPublicacao> ListarTodasAsPublicacoes();

        public Task<bool> ExcluirPublicacao(Guid idPublicacao);

        public ExibirPublicacao BuscarPublicacaoPorId(Guid idPublicacao);
        public List<ExibirPublicacao> ListarPublicacoesPorIdDoUsuario(Guid IdUsuario);
    }
}
