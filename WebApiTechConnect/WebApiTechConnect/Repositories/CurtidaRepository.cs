using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Curtida;

namespace WebApiTechConnect.Repositories
{
    public class CurtidaRepository : ICurtida
    {
        private readonly TechConnectContext _context;

        public CurtidaRepository()
        {
            _context = new TechConnectContext();
        }

        public bool CurtirPublicacao(CadastrarCurtida curtida)
        {

            List<Curtidas> curtidasBuscadas = _context.Curtidas.Where(x => x.IdPublicacao == curtida.IdPublicacao && x.IdUsuario == curtida.IdUsuario).ToList();

            if (curtidasBuscadas.Count > 0)
            {
                return false;
            }

            Curtidas novaCurtida = new()
            {
                IdPublicacao = curtida.IdPublicacao,
                IdUsuario = curtida.IdUsuario,
            };
            _context.Curtidas.Add(novaCurtida);
            _context.SaveChanges();

            return true;
        }

        public bool DescurtirPublicacao(Guid IdCurtida)
        {
            Curtidas curtidaBuscada = _context.Curtidas.FirstOrDefault(x => x.IdCurtida == IdCurtida)!;



            if (curtidaBuscada == null)
            {
                return false;
            }

            _context.Curtidas.Remove(curtidaBuscada);
            _context.SaveChanges();

            return true;
        }

        public List<ExibirCurtida> BuscarCurtidasDaPublicacaoPorIds(Guid IdPubublicacao, Guid IdUsuario)
        {
            List<Curtidas> curtidasBuscadas = _context.Curtidas
                .Include(x => x.Publicacoes).Include(x => x.Publicacoes!.Midia).Include(x => x.Publicacoes!.Usuarios).Include(x => x.Publicacoes!.Usuarios!.Midia)
                .Include(x => x.Usuarios).Include(x => x.Usuarios!.Midia)
                .Where(x => x.IdPublicacao == IdPubublicacao && x.IdUsuario == IdUsuario).ToList();

            List<ExibirCurtida> curtidasFormatadas = [];

            foreach (Curtidas curtida in curtidasBuscadas)
            {
                ExibirCurtida exibirCurtida = new()
                {
                    IdCurtida = curtida.IdCurtida,

                    Publicacao = new()
                    {
                        IdPublicacao = curtida.Publicacoes!.IdPublicacao,
                        Descricao = curtida.Publicacoes.Descricao,
                        DataPublicacao = curtida.Publicacoes.DataPublicacao,
                        UrlMidia = curtida.Publicacoes.Midia!.UrlMidia,

                        Usuario = new()
                        {
                            IdUsuario = curtida.Publicacoes.Usuarios!.IdUsuario,
                            Email = curtida.Publicacoes.Usuarios.Email,
                            GoogleId = curtida.Publicacoes.Usuarios.GoogleId,
                            Nome = curtida.Publicacoes.Usuarios.Nome,
                            UrlMidia = curtida.Publicacoes.Usuarios.Midia!.UrlMidia
                        }

                    },

                    Usuario = new()
                    {
                        IdUsuario = curtida.Usuarios!.IdUsuario,
                        Email = curtida.Usuarios.Email,
                        GoogleId = curtida.Usuarios.GoogleId,
                        Nome = curtida.Usuarios.Nome,
                        UrlMidia = curtida.Usuarios.Midia!.UrlMidia
                    }

                };

                curtidasFormatadas.Add(exibirCurtida);
            }


            return curtidasFormatadas;
        }

        public List<ExibirCurtida> BuscarCurtidasDaPublicacaoPorIdPublicacao(Guid IdPublicacao)
        {
            List<Curtidas> curtidasBuscadas = _context.Curtidas
               .Include(x => x.Publicacoes).Include(x => x.Publicacoes!.Midia).Include(x => x.Publicacoes!.Usuarios).Include(x => x.Publicacoes!.Usuarios!.Midia)
               .Include(x => x.Usuarios).Include(x => x.Usuarios!.Midia)
               .Where(x => x.IdPublicacao == IdPublicacao).ToList();

            List<ExibirCurtida> curtidasFormatadas = [];

            foreach (Curtidas curtida in curtidasBuscadas)
            {
                ExibirCurtida exibirCurtida = new()
                {
                    IdCurtida = curtida.IdCurtida,

                    Publicacao = new()
                    {
                        IdPublicacao = curtida.Publicacoes!.IdPublicacao,
                        Descricao = curtida.Publicacoes.Descricao,
                        DataPublicacao = curtida.Publicacoes.DataPublicacao,
                        UrlMidia = curtida.Publicacoes.Midia!.UrlMidia,

                        Usuario = new()
                        {
                            IdUsuario = curtida.Publicacoes.Usuarios!.IdUsuario,
                            Email = curtida.Publicacoes.Usuarios.Email,
                            GoogleId = curtida.Publicacoes.Usuarios.GoogleId,
                            Nome = curtida.Publicacoes.Usuarios.Nome,
                            UrlMidia = curtida.Publicacoes.Usuarios.Midia!.UrlMidia
                        }

                    },

                    Usuario = new()
                    {
                        IdUsuario = curtida.Usuarios!.IdUsuario,
                        Email = curtida.Usuarios.Email,
                        GoogleId = curtida.Usuarios.GoogleId,
                        Nome = curtida.Usuarios.Nome,
                        UrlMidia = curtida.Usuarios.Midia!.UrlMidia
                    }

                };

                curtidasFormatadas.Add(exibirCurtida);
            }


            return curtidasFormatadas;
        }
    }
}
