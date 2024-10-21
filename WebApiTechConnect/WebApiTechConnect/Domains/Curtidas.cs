using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTechConnect.Domains
{
    [Table("Curtidas")]
    public class Curtidas
    {
        [Key]
        public Guid IdCurtida { get; set; } = Guid.NewGuid();
        public Guid IdUsuario { get; set; }

        // Referência a table usuário
        [Required(ErrorMessage = "O usuário é obrigatório!")]
        [ForeignKey("IdUsuario")]
        public Usuarios? Usuarios { get; set; }

        public Guid IdPublicacao { get; set; }

        // Referência a table usuário
        [Required(ErrorMessage = "A publicacao é obrigatório!")]
        [ForeignKey("IdPublicacao")]
        public Publicacoes? Publicacoes { get; set; }
    }
}
