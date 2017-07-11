using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class UpVotePerguntaMap : EntityTypeConfiguration<UpVotePergunta>
    {
        public UpVotePerguntaMap()
        {
            ToTable("UpVotePergunta");

            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"))
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Pergunta)
                .WithMany()
                .Map(x => x.MapKey("IdPergunta"))
                .WillCascadeOnDelete(false);
        }
    }
}
