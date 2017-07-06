using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class RespostaMap : EntityTypeConfiguration<Resposta>
    {
        public RespostaMap()
        {
            ToTable("Repostas");
            HasKey(x => x.Id);

            /*HasRequired(x => x.Usuario)
                .WithMany(x => x.Respostas)
                .Map(x => x.MapKey("IdUsuario"));*/

            HasRequired(x => x.Pergunta)
                .WithMany(x => x.Respostas)
                .Map(x => x.MapKey("IdPergunta"))
                .WillCascadeOnDelete(false); 
            
        }
    }
}
