using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class DownVoteRespostaMap : EntityTypeConfiguration<DownVoteResposta>
    {
        public DownVoteRespostaMap()
        {
            ToTable("DownVoteResposta");

            HasRequired(x => x.Usuario)
                .WithMany()
                .Map(x => x.MapKey("IdUsuario"))
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Resposta)
                .WithMany()
                .Map(x => x.MapKey("IdResposta"))
                .WillCascadeOnDelete(false);
        }
    }
}
