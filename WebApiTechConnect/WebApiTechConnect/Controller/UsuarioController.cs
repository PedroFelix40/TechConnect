using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuario _usuario;

        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        }

        [HttpGet("BuscarPorId")]
        public IActionResult BuscarPorId(Guid idUsuario)
        {
            try
            {
                ExibirUsuario usuario = _usuario.BuscarUsuarioPorId(idUsuario);

                if (usuario == null)
                {
                    return StatusCode(404);
                }

                return StatusCode(200, usuario);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CadastrarUsuario user)
        {
            try
            {

                if (user == null)
                {
                    return BadRequest("Usuário inválido");
                }
                ExibirUsuario ususarioCdastrado = _usuario.Cadastrar(user);

                if (ususarioCdastrado == null)
                {
                    return BadRequest("Usuário já existe!");
                }

                return Ok(ususarioCdastrado);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            try
            {
                List<ExibirUsuario> listUsers = _usuario.ListarUsuarios();

                if (listUsers.Count > 0)
                {
                    return Ok(listUsers);
                }

                return NotFound("Não encontramos nenhum usuário.");


            }
            catch (Exception)
            {

                throw;
            }
        }
        
        
        [HttpGet("BuscarUsuarioPorEmailEGoogleId")]
        public IActionResult BuscarUsuarioPorEmailEGoogleId(string Email, string GoogleId)
        {
            try
            {
                ExibirUsuario usuario = _usuario.BuscarUsuarioPorEmailEGoogleId(Email, GoogleId);

                if (usuario == null)
                {
                    return StatusCode(404);
                }

                return StatusCode(200, usuario);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
