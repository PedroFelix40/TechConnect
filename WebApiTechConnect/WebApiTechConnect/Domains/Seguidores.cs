using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTechConnect.Domains
{

    [Table("Seguidores")]
    public class Seguidores
    {
        [Key]
        public Guid IdSeguidores { get; set; } = Guid.NewGuid();

        // Referência a table Usuario
        [Required(ErrorMessage = "O seguidor é obrigatório!")]
        public Guid IdSeguidor { get; set; }

        [ForeignKey("IdSeguidor")]
        public Usuarios? Seguidor { get; set; }


        // Referência a table Usuario
        [Required(ErrorMessage = "O usuário seguido é obrigatório!")]
        public Guid IdSeguido { get; set; }

        [ForeignKey("IdSeguido")]
        public Usuarios? Seguido { get; set; }
    }
}
