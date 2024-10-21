using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sprache;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Repositories;
using WebApiTechConnect.ViewModels.Curtida;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CurtidaController : ControllerBase
    {
        private readonly ICurtida _curtida;

        public CurtidaController(ICurtida curtida)
        {
            _curtida = curtida ?? throw new ArgumentNullException(nameof(curtida));
        }

        [HttpDelete]
        public IActionResult DescurtirPublicacao(Guid IdCurtida)
        {
            try
            {
                bool result = _curtida.DescurtirPublicacao(IdCurtida);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult CurtirPublicacao(CadastrarCurtida curtida)
        {
            try
            {
                if (curtida == null)
                {
                    return BadRequest("Curtida inválida!");
                }



                bool result = _curtida.CurtirPublicacao(curtida);

                if (!result)
                {
                    return StatusCode(400, "Você já curtiu essa publicação!");
                }

                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public IActionResult BuscarCurtidasDaPublicacaoPorIds(Guid IdPublicacao, Guid IdUsuario)
        {
            try
            {
                //valores nulos já tratados na repository :)
                List<ExibirCurtida> curtidas = _curtida.BuscarCurtidasDaPublicacaoPorIds(IdPublicacao, IdUsuario);

                //if (curtidas.Count > 0)
                //{
                //    //return StatusCode(200, curtidas);
                //}
                //return NotFound();

                return StatusCode(200, curtidas);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("BuscarCurtidasDaPublicacaoPorIdPublicacao")]
        public IActionResult BuscarCurtidasDaPublicacaoPorIdPublicacao(Guid IdPublicacao)
        {
            try
            {
                //valores nulos já tratados na repository :)
                List<ExibirCurtida> curtidas = _curtida.BuscarCurtidasDaPublicacaoPorIdPublicacao(IdPublicacao);

                //nao colocar validação para retornar status code

                //if (curtidas.Count > 0)
                //{
                //    return StatusCode(200, curtidas);
                //}
                //return NotFound();

                return StatusCode(200, curtidas);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
