using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.ViewModels.Comentario;

namespace WebApiTechConnect.Repositories
{
    public class ComentarioRepository : IComentario
    {
        private readonly TechConnectContext _context;

        public ComentarioRepository()
        {
            _context = new TechConnectContext();
        }

        public void CadastrarComentario(CadastrarComentario comentario)
        {
            Comentarios novoComentario = new()
            {
                Comentario = comentario.Comentario,
                DataPublicacao = DateTime.Now,
                IdPublicacao = comentario.IdPublicacao,
                IdUsuario = comentario.IdUsuario,
            };
            _context.Comentarios.Add(novoComentario);
            _context.SaveChanges();
        }

        public bool DeletarComentario(Guid IdComentario)
        {
            Comentarios comentarioBuscado = _context.Comentarios.FirstOrDefault(x => x.IdComentario == IdComentario)!;

            if (comentarioBuscado == null)
            {
                return false;
            }

            _context.Comentarios.Remove(comentarioBuscado);
            _context.SaveChanges();


            return true;

        }


        public List<ExibirComentario> ListarComentariosPeloIdDaPublicacao(Guid IdPublicacao)
        {
            List<Comentarios> comentariosBuscados = _context.Comentarios
                .Include(x => x.Publicacoes).Include(x => x.Publicacoes!.Midia).Include(x => x.Publicacoes!.Usuarios).Include(x => x.Publicacoes!.Usuarios!.Midia)
                .Include(x => x.Usuarios).Include(x => x.Usuarios!.Midia)
                .Where(x => x.IdPublicacao == IdPublicacao).ToList();

            List<ExibirComentario> comentariosFormatados = [];


            foreach (Comentarios comentario in comentariosBuscados)
            {

                ExibirComentario exibirComentario = new()
                {
                    IdComentario = comentario.IdComentario,
                    Comentario = comentario.Comentario,
                    DataPublicacao = comentario.DataPublicacao,


                    Publicacao = new()
                    {
                        IdPublicacao = comentario.Publicacoes!.IdPublicacao,
                        Descricao = comentario.Publicacoes.Descricao,
                        DataPublicacao = comentario.Publicacoes.DataPublicacao,
                        UrlMidia = comentario.Publicacoes.Midia!.UrlMidia,

                        Usuario = new()
                        {
                            IdUsuario = comentario.Publicacoes.Usuarios!.IdUsuario,
                            Email = comentario.Publicacoes.Usuarios.Email,
                            GoogleId = comentario.Publicacoes.Usuarios.GoogleId,
                            Nome = comentario.Publicacoes.Usuarios.Nome,
                            UrlMidia = comentario.Publicacoes.Usuarios.Midia!.UrlMidia
                        }

                    },

                    Usuario = new()
                    {
                        IdUsuario = comentario.Usuarios!.IdUsuario,
                        Email = comentario.Usuarios.Email,
                        GoogleId = comentario.Usuarios.GoogleId,
                        Nome = comentario.Usuarios.Nome,
                        UrlMidia = comentario.Usuarios.Midia!.UrlMidia
                    }

                };


                comentariosFormatados.Add(exibirComentario);
            }

            return comentariosFormatados;
        }
    }
}
