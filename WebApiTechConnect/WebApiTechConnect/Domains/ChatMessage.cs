using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTechConnect.Domains
{
    [Table("ChatMessage")]
    public class ChatMessage
    {
        [Key]
        public Guid IdChatMessage { get; set; } = Guid.NewGuid();

        [Column(TypeName = "TEXT")]
        [Required(ErrorMessage = "A mensagem é obrigatória!")]
        public string? Mensagem { get; set; }

        [Column(TypeName = "DATETIME")]
        [Required(ErrorMessage = "A data é obrigatória!")]
        public DateTime DataHoraEnvio { get; private set; }

        // Referência a table Usuario
        [Required(ErrorMessage = "O chat é obrigatório!")]
        public Guid IdChat { get; set; }

        [ForeignKey("IdChat")]
        public Chat? Chat { get; set; }

        // Referência a table Usuario
        [Required(ErrorMessage = "O remetente é obrigatório!")]
        public Guid IdRemetente { get; set; }

        [ForeignKey("IdRemetente")]
        public Usuarios? Remetente { get; set; }


        public ChatMessage()
        {
            this.DataHoraEnvio = DateTime.Now;
        }

    }
}
