using API_FitTrack.Utils;
using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Utils.BlobStorage;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly TechConnectContext _context;

        public UsuarioRepository()
        {
            _context = new TechConnectContext();
        }

        public ExibirUsuario BuscarUsuarioPorId(Guid IdUsuario)
        {
            try
            {
                Usuarios usuarioBuscado = _context.Usuarios.Include(x => x.Midia).FirstOrDefault(x => x.IdUsuario == IdUsuario);

                if (usuarioBuscado == null)
                {
                    return null;
                }

                ExibirUsuario usuario = new()
                {
                    IdUsuario = usuarioBuscado.IdUsuario,
                    Email = usuarioBuscado.Email,
                    Nome = usuarioBuscado.Nome,
                    GoogleId = usuarioBuscado.GoogleId,
                    UrlMidia = usuarioBuscado.Midia!.UrlMidia,
                };

                return usuario;

            }
            catch (Exception)
            {

                throw;
            }
        }


        public ExibirUsuario Cadastrar(CadastrarUsuario usuario)
        {
            try
            {
                bool emailEValido = EmailValidator.IsValidEmail(usuario.Email!);

                if (!emailEValido)
                {
                    return null;
                }

                Usuarios usuarioBuscado = _context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email)!;

                if (usuarioBuscado != null)
                {
                    return null;
                }

                Midia novaMidiaUsuario = new()
                {
                    UrlMidia = usuario.UrlMidia,
                    BlobStorageName = "ProfilePicture" + Guid.NewGuid().ToString().Replace("-", ""),
                };

                Usuarios novoUsuario = new()
                {
                    Email = usuario.Email,
                    GoogleId = Criptografia.GerarHash(usuario.GoogleId!),
                    Nome = usuario.Nome,
                    IdMidia = novaMidiaUsuario.IdMidia
                };

                _context.Midia.Add(novaMidiaUsuario);
                _context.Usuarios.Add(novoUsuario);
                _context.SaveChanges();


                return new ExibirUsuario
                {
                    IdUsuario = novoUsuario.IdUsuario,
                    Email = usuario.Email,
                    GoogleId = usuario.GoogleId,
                    Nome = usuario.Nome,
                    UrlMidia = novaMidiaUsuario.UrlMidia,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ExibirUsuario> ListarUsuarios()
        {
            try
            {
                List<Usuarios> usuariosBuscados = _context.Usuarios.Include(x => x.Midia).ToList();

                List<ExibirUsuario> usuariosFormatados = [];

                foreach (Usuarios usuario in usuariosBuscados)
                {
                    ExibirUsuario exibirUsuario = new()
                    {
                        Email = usuario.Email,
                        GoogleId = usuario.GoogleId,
                        IdUsuario = usuario.IdUsuario,
                        Nome = usuario.Nome,
                        UrlMidia = usuario.Midia!.UrlMidia
                    };

                    usuariosFormatados.Add(exibirUsuario);
                }

                return usuariosFormatados;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ExibirUsuario BuscarUsuarioPorEmailEGoogleId(string Email, string GoogleId)
        {
            try
            {
                Usuarios usuarioBuscado = _context.Usuarios.Include(x => x.Midia).FirstOrDefault(x => x.Email == Email)!;

                if (usuarioBuscado == null)
                {
                    return null;
                }

                if (!Criptografia.CompararHash(GoogleId, usuarioBuscado.GoogleId!))
                {
                    return null;
                }

                ExibirUsuario usuarioFormatado = new()
                {
                    IdUsuario = usuarioBuscado.IdUsuario,
                    Nome = usuarioBuscado.Nome,
                    Email = usuarioBuscado.Email,
                    GoogleId = usuarioBuscado.GoogleId,
                    UrlMidia = usuarioBuscado.Midia!.UrlMidia,

                };


                return usuarioFormatado;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
