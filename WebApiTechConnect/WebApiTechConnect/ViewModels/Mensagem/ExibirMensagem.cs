using WebApiTechConnect.Domains;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.ViewModels.Mensagem
{
    public class ExibirMensagem
    {
        public Guid IdMensagem { get; set; }
        public DateTime DataHoraEnvio { get; set; }
        public string? Mensagem { get; set; }
        public ExibirUsuarioMensagem? Remetente { get; set; }
        public ExibirUsuarioMensagem? Destinatario { get; set; }
    }
}
