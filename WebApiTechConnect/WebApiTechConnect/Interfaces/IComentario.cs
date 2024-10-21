using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Comentario;

namespace WebApiTechConnect.Interfaces
{
    public interface IComentario
    {
        void CadastrarComentario(CadastrarComentario comentario);
        bool DeletarComentario(Guid IdComentario);
        List<ExibirComentario> ListarComentariosPeloIdDaPublicacao(Guid IdPublicacao);

    }
}
