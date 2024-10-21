namespace WebApiTechConnect.ViewModels.Mensagem
{
    public class CadastrarMensagem
    {
        public string? Mensagem { get; set; }
        public Guid IdChat { get; set; }
        public Guid IdRemetente { get; set; }
    }
}
