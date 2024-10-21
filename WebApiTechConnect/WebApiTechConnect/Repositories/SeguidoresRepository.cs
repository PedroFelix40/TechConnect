using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Usuario;

namespace WebApiTechConnect.Repositories
{
    public class SeguidoresRepository : ISeguidores
    {

        private readonly TechConnectContext _context;

        public SeguidoresRepository()
        {
            _context = new TechConnectContext();
        }
        public List<ExibirUsuario> BuscarSeguidores(Guid idUsuario)
        {
            List<Seguidores> seguidoresBuscados = _context.Seguidores
                .Include(x => x.Seguidor).Include(x => x.Seguidor!.Midia)
                .Include(x => x.Seguido).Include(x => x.Seguido!.Midia)
                .Where(x => x.IdSeguido == idUsuario).ToList();

            List<ExibirUsuario> seguidores = [];

            foreach (Seguidores item in seguidoresBuscados)
            {
                ExibirUsuario exibirUsuario = new()
                {
                    IdUsuario = item.Seguidor!.IdUsuario,
                    Email = item.Seguidor!.Email,
                    GoogleId = item.Seguidor!.GoogleId,
                    Nome = item.Seguidor!.Nome,
                    UrlMidia = item.Seguidor!.Midia!.UrlMidia,
                };

                seguidores.Add(exibirUsuario);
            }

            return seguidores;
        }

        public List<ExibirUsuario> BuscarSeguindo(Guid idUsuario)
        {
            List<Seguidores> usuariosSeguidosBuscados = _context.Seguidores
                 .Include(x => x.Seguidor).Include(x => x.Seguidor!.Midia)
                 .Include(x => x.Seguido).Include(x => x.Seguido!.Midia)
                 .Where(x => x.IdSeguidor == idUsuario).ToList();

            List<ExibirUsuario> usuariosSeguidos = [];

            foreach (Seguidores item in usuariosSeguidosBuscados)
            {
                ExibirUsuario exibirUsuario = new()
                {
                    IdUsuario = item.Seguido!.IdUsuario,
                    Email = item.Seguido!.Email,
                    GoogleId = item.Seguido!.GoogleId,
                    Nome = item.Seguido!.Nome,
                    UrlMidia = item.Seguido!.Midia!.UrlMidia,
                };

                usuariosSeguidos.Add(exibirUsuario);
            }

            return usuariosSeguidos;
        }

        public bool PararDeSeguir(Guid idSeguidor, Guid idSeguido)
        {
            Seguidores seguidorBuscado = _context.Seguidores.FirstOrDefault(x => (x.IdSeguidor == idSeguidor && x.IdSeguido == idSeguido))!;

            if (seguidorBuscado == null)
            {
                return false;
            }

            _context.Seguidores.Remove(seguidorBuscado);
            _context.SaveChanges();

            return true;

        }

        public bool Seguir(Guid idSeguidor, Guid idSeguido)
        {

            Seguidores seguidorBuscado = _context.Seguidores.FirstOrDefault(x => x.IdSeguidor == idSeguidor && x.IdSeguido == idSeguido)!;

            if (seguidorBuscado != null)
            {
                return false;
            }

            Seguidores novoSeguidor = new()
            {
                IdSeguidor = idSeguidor,
                IdSeguido = idSeguido
            };

            _context.Seguidores.Add(novoSeguidor);
            _context.SaveChanges();

            return true;
        }
    }
}
