using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiTechConnect.Domains
{
    [Table("Chat")]
    public class Chat
    {
        [Key]
        public Guid IdChat { get; set; }


        // Referência a table Usuario
        [Required(ErrorMessage = "O usuário 1 é obrigatório!")]
        public Guid IdUsuario1 { get; set; }

        [ForeignKey("IdUsuario1")]
        public Usuarios? Usuario1 { get; set; }


        // Referência a table Usuario
        [Required(ErrorMessage = "O usuário 2 é obrigatório!")]
        public Guid IdUsuario2 { get; set; }

        [ForeignKey("IdUsuario2")]
        public Usuarios? Usuario2 { get; set; }
    }
}
