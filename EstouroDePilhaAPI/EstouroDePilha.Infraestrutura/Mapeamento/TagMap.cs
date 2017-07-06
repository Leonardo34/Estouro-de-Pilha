using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            ToTable("Tag");

            /*HasMany(x => x.Perguntas)
                .WithMany(x => x.Tags)
                .Map(x =>
                {
                    x.MapLeftKey("IdTag");
                    x.MapRightKey("IdPergunta");
                    x.ToTable("TagPergunta");
                });
           */
        }
    }
}
