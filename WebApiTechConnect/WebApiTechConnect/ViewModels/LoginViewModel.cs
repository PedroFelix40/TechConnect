using System.ComponentModel.DataAnnotations;

namespace WebApiTechConnect.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        public string? Email { get; set; }

        public string? GoogleIdAccount { get; set; }
    }
}
