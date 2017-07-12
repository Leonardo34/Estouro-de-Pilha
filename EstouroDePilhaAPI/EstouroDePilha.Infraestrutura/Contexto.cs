using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Infraestrutura.Mapeamento;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura
{
    public class Contexto : DbContext
    {
        public static Contexto contexto = new Contexto();

        public Contexto() : base("ExemploEFSP")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UpVoteResposta> UpVotesResposta { get; set; }
        public DbSet<DownVoteResposta> DownVotesResposta { get; set; }
        public DbSet<DownVotePergunta> DownVotesPerguntas { get; set; }
        public DbSet<UpVotePergunta> UpVotesPerguntas { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<ComentarioResposta> ComentariosRespostas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RespostaMap());
            modelBuilder.Configurations.Add(new PerguntaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new UpVoteRespostaMap());
            modelBuilder.Configurations.Add(new DownVoteRespostaMap());
            modelBuilder.Configurations.Add(new BadgeMap());
            modelBuilder.Configurations.Add(new ComentarioRespostaMap());
        }
    }
}
