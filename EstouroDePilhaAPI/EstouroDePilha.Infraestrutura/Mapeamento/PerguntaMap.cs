using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class PerguntaMap : EntityTypeConfiguration<Pergunta>
    {
        public PerguntaMap()
        {
            ToTable("Pergunta");

            HasKey(x => x.Id);

            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("Id"));

            HasMany(x => x.Respostas)
                .WithRequired(x => x.Pergunta)
                .Map(x => x.MapKey("Id"));

            HasMany(x => x.Tags)
                .WithMany(x => x.Perguntas)
                .Map(x =>
                {
                    x.MapLeftKey("IdPergunta");
                    x.MapRightKey("IdTag");
                    x.ToTable("TagPergunta");
                });
        }
    }
}
