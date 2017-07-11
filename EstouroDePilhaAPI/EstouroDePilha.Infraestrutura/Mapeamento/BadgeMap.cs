using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class BadgeMap : EntityTypeConfiguration<Badge>
    {
        public BadgeMap()
        {
            ToTable("Badge");
        }
    }
}
