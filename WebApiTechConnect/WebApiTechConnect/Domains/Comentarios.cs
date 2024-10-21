using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTechConnect.Domains
{
    [Table("Comentarios")]
    public class Comentarios
    {
        [Key]
        public Guid IdComentario { get; set; } = Guid.NewGuid();

        [Column(TypeName = "TEXT")]
        [Required(ErrorMessage = "O comentario da publicação é obrigatória!")]
        public string? Comentario { get; set; }


        [Column(TypeName = "DATE")]
        [Required(ErrorMessage = "A data do comentario é obrigatório!")]
        public DateTime? DataPublicacao { get; set; }


        // Referência a table usuário
        [Required(ErrorMessage = "O usuário é obrigatório!")]
        public Guid IdUsuario { get; set; }


        [ForeignKey("IdUsuario")]
        public Usuarios? Usuarios { get; set; }



        // Referência a table usuário
        [Required(ErrorMessage = "A publicacao é obrigatório!")]
        public Guid IdPublicacao { get; set; }


        [ForeignKey("IdPublicacao")]
        public Publicacoes? Publicacoes { get; set; }
    }
}
