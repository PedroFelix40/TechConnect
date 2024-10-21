using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiTechConnect.ViewModels.Publicacao
{
    public class CadastrarPublicacao
    {
        public string? Descricao { get; set; }
        public Guid IdUsuario { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile? Arquivo { get; set; }
    }
}
