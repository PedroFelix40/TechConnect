using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Comentario;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentario _comentario;

        //armazena os dados da API externa (AI)
        private readonly ContentModeratorClient _contentModeratorClient;

        public ComentarioController(IComentario comentario, ContentModeratorClient contentModeratorClient)
        {
            _comentario = comentario ?? throw new ArgumentNullException(nameof(comentario));
            _contentModeratorClient = contentModeratorClient ?? throw new ArgumentNullException(nameof(contentModeratorClient));
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarComentario(CadastrarComentario comentario)
        {
            try
            {
                if (comentario == null || string.IsNullOrWhiteSpace(comentario.Comentario))
                {
                    return BadRequest("Informe um comentário válido!");
                }

                //converte a string (descricao do comentario) em um MemoryStream
                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(comentario.Comentario!));

                //realiza a moderação do conteudo (descrição do comentario)
                var moderationResult = await _contentModeratorClient.TextModeration
                    .ScreenTextAsync("text/plain", stream, "por", false, false, null, true);

                //se existir termos ofensivos
                if (moderationResult.Terms != null)
                {
                    return BadRequest("Comentário ofensivo!");
                }
                _comentario.CadastrarComentario(comentario);

                return Ok("Cadastrado com sucesso");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public IActionResult Delete(Guid IdComentario)
        {
            try
            {
                bool result = _comentario.DeletarComentario(IdComentario);

                if (!result)
                {
                    return StatusCode(404);
                }

                return Ok("Comentário excluido com sucesso.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult ListarComentarios(Guid idPublicacao)
        {
            try
            {
                List<ExibirComentario> comentarios = _comentario.ListarComentariosPeloIdDaPublicacao(idPublicacao);

                return Ok(comentarios);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
