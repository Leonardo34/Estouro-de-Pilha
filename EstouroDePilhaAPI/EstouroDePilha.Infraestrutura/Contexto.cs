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

        }

        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RespostaMap());
            modelBuilder.Configurations.Add(new PerguntaMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new TagMap());
        }
    }
}
