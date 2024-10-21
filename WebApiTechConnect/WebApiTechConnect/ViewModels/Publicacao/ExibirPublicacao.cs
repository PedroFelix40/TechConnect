using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.ViewModels.Publicacao
{
    public class ExibirPublicacao
    {
        public Guid IdPublicacao { get; set; }
        public string? Descricao { get; set; }
        public string? UrlMidia { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public ExibirUsuario? Usuario { get; set; }
    }
}
