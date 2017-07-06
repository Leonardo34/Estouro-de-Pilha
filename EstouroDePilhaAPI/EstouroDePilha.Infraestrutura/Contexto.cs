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
        public Contexto() : base("ExemploEFSP")
        {

        }

        public DbSet<Resposta> Respostas { get; set; }
        public DbSet<Pergunta> Perguntas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RespostaMap());
            modelBuilder.Configurations.Add(new PerguntaMap());
        }

    }
}
