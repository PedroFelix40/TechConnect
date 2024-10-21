using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Usuario;
using WebApiTechConnect.ViewModels.Publicacao;

namespace WebApiTechConnect.ViewModels.Comentario
{
    public class ExibirComentario
    {
        public Guid IdComentario { get; set; }
        public string? Comentario { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public ExibirUsuario? Usuario { get; set; }
        public ExibirPublicacao? Publicacao { get; set; }
    }
}
