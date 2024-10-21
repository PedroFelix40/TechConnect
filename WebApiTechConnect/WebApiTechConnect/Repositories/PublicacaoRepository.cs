using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Contexts;
using WebApiTechConnect.Domains;
using WebApiTechConnect.Interfaces;
using WebApiTechConnect.Utils.BlobStorage;
using WebApiTechConnect.ViewModels.Publicacao;

namespace WebApiTechConnect.Repositories
{
    public class PublicacaoRepository : IPublicacao
    {
        private readonly TechConnectContext _context;

        public PublicacaoRepository()
        {
            _context = new TechConnectContext();
        }

        public async Task CadastrarPublicacao(CadastrarPublicacao publicacao)
        {
            try
            {
                Midia novaMidiaPublicacao = await AzureBlobStorageHelper.UploadImageBlobAsync(publicacao.Arquivo!);

                Publicacoes novaPublicacao = new()
                {
                    DataPublicacao = DateTime.Now,
                    Descricao = publicacao.Descricao,
                    IdUsuario = publicacao.IdUsuario,
                    IdMidia = novaMidiaPublicacao.IdMidia,

                };

                _context.Midia.Add(novaMidiaPublicacao);
                _context.Publicacoes.Add(novaPublicacao);
                _context.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EditarPublicacao(AtualizarPublicacao publicacao)
        {
            throw new NotImplementedException();
        }

        public List<ExibirPublicacao> ListarTodasAsPublicacoes()
        {
            List<Publicacoes> publicacoesBuscadas = _context.Publicacoes.Include(x => x.Midia).Include(x => x.Usuarios)
                .Include(x => x.Usuarios!.Midia)
                .ToList();

            List<ExibirPublicacao> publicacoesFormatadas = [];


            foreach (Publicacoes publicacao in publicacoesBuscadas)
            {
                ExibirPublicacao exibirPublicacao = new()
                {
                    IdPublicacao = publicacao.IdPublicacao,
                    DataPublicacao = publicacao.DataPublicacao,
                    Descricao = publicacao.Descricao,
                    UrlMidia = publicacao.Midia!.UrlMidia,
                    Usuario = new()
                    {
                        Email = publicacao.Usuarios!.Email,
                        IdUsuario = publicacao.Usuarios.IdUsuario,
                        GoogleId = publicacao.Usuarios.GoogleId,
                        Nome = publicacao.Usuarios.Nome,
                        UrlMidia = publicacao.Usuarios.Midia!.UrlMidia,
                    }
                };

                publicacoesFormatadas.Add(exibirPublicacao);
            }

            return publicacoesFormatadas;
        }

        public async Task<bool> ExcluirPublicacao(Guid idPublicacao)
        {
            Publicacoes publicacaoBuscada = _context.Publicacoes.Include(x => x.Midia).FirstOrDefault(x => x.IdPublicacao == idPublicacao)!;

            if (publicacaoBuscada == null)
            {
                return false;
            }

            Midia midiaBuscada = _context.Midia.FirstOrDefault(x => x.IdMidia == publicacaoBuscada.Midia!.IdMidia)!;

            //exclui a foto da publicacao do blobstorage
            await AzureBlobStorageHelper.DeleteBlobAsync(publicacaoBuscada.Midia!.BlobStorageName!);

            _context.Publicacoes.Remove(publicacaoBuscada);
            _context.Midia.Remove(midiaBuscada);
            _context.SaveChanges();

            return true;
        }

        public ExibirPublicacao BuscarPublicacaoPorId(Guid idPublicacao)
        {
            Publicacoes publicacaoBuscada = _context.Publicacoes.Include(x => x.Usuarios).Include(x => x.Midia)
                .Include(x => x.Usuarios!.Midia)
                .FirstOrDefault(x => x.IdPublicacao == idPublicacao)!;

            if (publicacaoBuscada == null)
            {
                return null;
            }

            ExibirPublicacao publicacaoFormatada = new()
            {
                IdPublicacao = publicacaoBuscada.IdPublicacao,
                DataPublicacao = publicacaoBuscada.DataPublicacao,
                Descricao = publicacaoBuscada.Descricao,
                UrlMidia = publicacaoBuscada.Midia!.UrlMidia,

                Usuario = new()
                {
                    IdUsuario = publicacaoBuscada.Usuarios!.IdUsuario,
                    Email = publicacaoBuscada.Usuarios.Email,
                    GoogleId = publicacaoBuscada.Usuarios.GoogleId,
                    Nome = publicacaoBuscada.Usuarios.Nome,
                    UrlMidia = publicacaoBuscada.Usuarios.Midia!.UrlMidia,
                }
            };


            return publicacaoFormatada;
        }

        public List<ExibirPublicacao> ListarPublicacoesPorIdDoUsuario(Guid IdUsuario)
        {
            List<Publicacoes> publicacoesBuscadas = _context.Publicacoes.Include(x => x.Midia).Include(x => x.Usuarios)
                .Include(x => x.Usuarios!.Midia)
               .Where(x => x.IdUsuario == IdUsuario).ToList();

            List<ExibirPublicacao> publicacoesFormatadas = [];

            foreach (Publicacoes publicacao in publicacoesBuscadas)
            {
                ExibirPublicacao exibirPublicacao = new()
                {
                    IdPublicacao = publicacao.IdPublicacao,
                    DataPublicacao = publicacao.DataPublicacao,
                    Descricao = publicacao.Descricao,
                    UrlMidia = publicacao.Midia!.UrlMidia,
                    Usuario = new()
                    {
                        Email = publicacao.Usuarios!.Email,
                        IdUsuario = publicacao.Usuarios.IdUsuario,
                        GoogleId = publicacao.Usuarios.GoogleId,
                        Nome = publicacao.Usuarios.Nome,
                        UrlMidia = publicacao.Usuarios.Midia!.UrlMidia,
                    }
                };

                publicacoesFormatadas.Add(exibirPublicacao);
            }

            return publicacoesFormatadas;
        }
    }
}
