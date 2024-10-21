using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTechConnect.Domains
{
    [Table("Publicacoes")]
    public class Publicacoes
    {
        [Key]
        public Guid IdPublicacao { get; set; } = Guid.NewGuid();

        [Column(TypeName = "TEXT")]
        [Required(ErrorMessage = "A descricao da publicação é obrigatória!")]
        public string? Descricao { get; set; }


        [Column(TypeName = "DATE")]
        [Required(ErrorMessage = "A data da publicacao é obrigatório!")]
        public DateTime? DataPublicacao { get; set; }


        public Guid IdMidia { get; set; }

        // Referência a table Midia
        [Required(ErrorMessage = "A mídia é obrigatória!")]
        [ForeignKey("IdMidia")]
        public Midia? Midia { get; set; }

        // Fazendo referência a tabela Usuário

        [Required(ErrorMessage = "O usuário é obrigatório!")]
        public Guid IdUsuario { get; set; }


        [ForeignKey("IdUsuario")]
        public Usuarios? Usuarios { get; set; }
    }
}
