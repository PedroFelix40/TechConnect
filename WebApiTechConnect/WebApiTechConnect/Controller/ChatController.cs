using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Chat;
using WebApiTechConnect.ViewModels.Comentario;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ChatController : ControllerBase
    {

        private readonly IChat _chat;

        public ChatController(IChat chat)
        {
            _chat = chat ?? throw new ArgumentNullException(nameof(chat));
        }

        [HttpPost]
        public IActionResult CriarChat([FromBody] CadastrarChat cadastrarChat)
        {
            try
            {
                if (string.IsNullOrEmpty(cadastrarChat.IdUsuario1.ToString()) || string.IsNullOrEmpty(cadastrarChat.IdUsuario2.ToString()))
                {
                    return BadRequest("Informe usuários válidos!");
                }

                bool result = _chat.CriarChat(cadastrarChat.IdUsuario1, cadastrarChat.IdUsuario2);

                if (!result)
                {
                    return BadRequest("Erro ao criar!");
                }

                return StatusCode(201, "Cadastrado com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult ExcluirChat(Guid idChat)
        {
            try
            {
                if (string.IsNullOrEmpty(idChat.ToString()))
                {
                    return BadRequest("Informe um chat válids!");
                }

                bool result = _chat.ExcluirChat(idChat);

                if (!result)
                {
                    return BadRequest("Erro ao excluir");
                }

                return StatusCode(204);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult BuscarChatsDoUsuario(Guid idUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(idUsuario.ToString()))
                {
                    return BadRequest("Informe um chat válids!");
                }

                List<ExibirChat> chats = _chat.BuscarChatsDoUsuario(idUsuario);

                return StatusCode(200, chats);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("BuscarChatPorIdDoUsuario")]
        public IActionResult BuscarChatPorIdDoUsuario(Guid idUsuario1, Guid idUsuario2)
        {
            try
            {
                if (string.IsNullOrEmpty(idUsuario1.ToString()) || (string.IsNullOrEmpty(idUsuario2.ToString())))
                {
                    return BadRequest("Informe um chat válids!");
                }

                ExibirChat chat = _chat.BuscarChatPorIdDoUsuario(idUsuario1, idUsuario2);

                return StatusCode(200, chat);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
