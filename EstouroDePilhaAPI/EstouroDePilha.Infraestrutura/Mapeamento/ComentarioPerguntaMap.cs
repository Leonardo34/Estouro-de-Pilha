using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    class ComentarioPerguntaMap : EntityTypeConfiguration<ComentarioPergunta>
    {
        public ComentarioPerguntaMap()
        {
            ToTable("ComentarioPerguntas");

            HasKey(x => x.Id);

            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"));

            HasRequired(x => x.Pergunta)
                .WithMany()
                .Map(x => x.MapKey("IdPergunta"))
                .WillCascadeOnDelete(false);
        }
    }
}
