using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SeguidoresController : ControllerBase
    {
        private ISeguidores _seguidores;

        public SeguidoresController(ISeguidores seguidores)
        {
            _seguidores = seguidores ?? throw new ArgumentNullException(nameof(seguidores));
        }

        [HttpGet("BuscarSeguidores")]
        public IActionResult BuscarSeguidores(Guid idUsuario)
        {
            try
            {
                List<ExibirUsuario> seguidores = _seguidores.BuscarSeguidores(idUsuario);

                return StatusCode(200, seguidores);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        [HttpGet("BuscarSeguindo")]
        public IActionResult BuscarSeguindo(Guid idUsuario)
        {
            try
            {
                List<ExibirUsuario> seguindo = _seguidores.BuscarSeguindo(idUsuario);

                return StatusCode(200, seguindo);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        public IActionResult Seguir(Guid idSeguidor, Guid idSeguido)
        {
            try
            {
                bool result = _seguidores.Seguir(idSeguidor, idSeguido);

                if (!result)
                {
                    return NotFound();
                }

                return StatusCode(201);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        public IActionResult PararDeSeguir(Guid idSeguidor, Guid idSeguido)
        {
            try
            {
                bool result = _seguidores.PararDeSeguir(idSeguidor, idSeguido);

                if (!result)
                {
                    return NotFound();
                }

                return StatusCode(201);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
