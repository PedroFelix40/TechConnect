using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Repositories;
using WebApiTechConnect.ViewModels;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;

        public LoginController()
        {
            _usuario = new UsuarioRepository();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel usuario)
        {
            try
            {
                //busca usuário por email e senha ou por email e id da conta google, caso esteja logando com o goole
                ExibirUsuario usuarioBuscado = _usuario.BuscarUsuarioPorEmailEGoogleId(usuario.Email!, usuario.GoogleIdAccount!);

                if (usuarioBuscado == null)
                {
                    return StatusCode(404, "Usuário não encontrado!");
                }


                //caso encontre, prossegue para a criação do token

                //informações que serão fornecidas no token
                Claim[] claims =
                [
                    new (JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),
                    new (JwtRegisteredClaimNames.Name,usuarioBuscado.Nome!),
                    new (JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    //new ("googleId", usuarioBuscado.GoogleId!),
                    new ("urlMidia", usuarioBuscado.UrlMidia!),
                ];

                //chave de segurança
                SymmetricSecurityKey key = new(System.Text.Encoding.UTF8.GetBytes("techconnect-webapi-chave-autenticacao-ef"));

                //credenciais
                SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

                //token
                JwtSecurityToken meuToken = new(
                        issuer: "webapi.techconnect",
                        audience: "webapi.techconnect",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
