using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTechConnect.Domains
{
    /// <summary>
    /// Tabela para armazenar a uri e blobname das imagens dos usuarios e das publicacoes
    /// </summary>
    public class Midia
    {
        [Key]
        public Guid IdMidia { get; set; } = Guid.NewGuid();

        [Column(TypeName = "VARCHAR(200)")]
        [Required(ErrorMessage = "A URL é obrigatória!")]
        public string? UrlMidia { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        [Required(ErrorMessage = "O nome do blob é obrigatório!")]
        public string? BlobStorageName { get; set; }
    }
}
