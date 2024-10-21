using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.ContentModerator;
using System.Text;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Repositories;
using WebApiTechConnect.Utils.BlobStorage;
using WebApiTechConnect.ViewModels.Publicacao;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PublicacaoController : ControllerBase
    {
        private readonly IPublicacao _publicacao;

        //armazena os dados da API externa (AI)
        private readonly ContentModeratorClient _contentModeratorClient;

        public PublicacaoController(IPublicacao publicacao, ContentModeratorClient contentModeratorClient)
        {
            _publicacao = publicacao ?? throw new ArgumentNullException(nameof(publicacao));
            _contentModeratorClient = contentModeratorClient ?? throw new ArgumentNullException(nameof(contentModeratorClient));

        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirPublicacao(Guid idPublicacao)
        {
            try
            {
                bool result = await _publicacao.ExcluirPublicacao(idPublicacao);

                if (!result)
                {
                    return StatusCode(404);
                }

                return StatusCode(204);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult ListarTodasAsPublicacoes()
        {
            try
            {
                List<ExibirPublicacao> publicacoes = _publicacao.ListarTodasAsPublicacoes();

                if (publicacoes.Count == 0)
                {
                    return NotFound("Essa lista está vazia");
                }

                return Ok(publicacoes);
            }
            catch (Exception)
            {

                throw;
            }
        }



        [HttpGet("buscarPorId")]
        public IActionResult BuscarPublicacaoPorId(Guid IdPublicacao)
        {
            try
            {
                ExibirPublicacao publicacao = _publicacao.BuscarPublicacaoPorId(IdPublicacao);

                if (publicacao != null)
                {
                    return Ok(publicacao);
                }

                return NotFound("Não há publicacoes com esse id");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("ListarPublicacoesPorIdDoUsuario")]
        public IActionResult ListarPublicacoesPorIdDoUsuario(Guid idUsuario)
        {
            try
            {
                List<ExibirPublicacao> publicacoes = _publicacao.ListarPublicacoesPorIdDoUsuario(idUsuario);

                if (publicacoes.Count > 0)
                {
                    return Ok(publicacoes);
                }

                return NotFound("Não há publicacoes com esse id");
            }
            catch (Exception)
            {

                throw;
            }
        }


        //public async Task<IActionResult> Post
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CadastrarPublicacao publicacao)
        {
            try
            {
                if (publicacao != null && publicacao.Arquivo != null)
                {

                    //converte a string (descricao do comentario) em um MemoryStream
                    using var stream = new MemoryStream(Encoding.UTF8.GetBytes(publicacao.Descricao!));

                    //realiza a moderação do conteudo (descrição do comentario)
                    var moderationResult = await _contentModeratorClient.TextModeration
                        .ScreenTextAsync("text/plain", stream, "por", false, false, null, true);

                    //se existir termos ofensivos
                    if (moderationResult.Terms != null)
                    {
                        return BadRequest("Descrição ofensiva!");
                    }


                    await _publicacao.CadastrarPublicacao(publicacao);
                    return StatusCode(201);
                }
                return BadRequest("Publicacao inválida!");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
