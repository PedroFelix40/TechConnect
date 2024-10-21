using Microsoft.EntityFrameworkCore;
using WebApiTechConnect.Domains;

namespace WebApiTechConnect.Contexts
{
    public class TechConnectContext : DbContext
    {
        public TechConnectContext()
        {

        }

        public TechConnectContext(DbContextOptions<TechConnectContext> options) : base(options)
        {
        }

        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Curtidas> Curtidas { get; set; }
        public DbSet<Publicacoes> Publicacoes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Midia> Midia { get; set; }
        public DbSet<Seguidores> Seguidores { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }

        private readonly string ConnectStringDataBase = Environment.GetEnvironmentVariable("ConnectStringDataBase")!;

        //"Data Source=NOTE14-SALA19; initial catalog=TechConnection; TrustServerCertificate=true; user Id = sa; pwd=Senai@134";

        //private readonly string StringConexao = "Data Source=NOTE09-SALA21; initial catalog=TechConnection; TrustServerCertificate=true; user Id = sa; pwd=Senai@134";

        // StringConexao
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseSqlServer(ConnectStringDataBase);
    }
}
