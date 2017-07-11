using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Mapeamento
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasMany(x => x.Perguntas)
                .WithRequired(x => x.Usuario)
                .Map(x => x.MapKey("IdUsuario"));

            HasMany(x => x.Respostas)
                .WithRequired(x => x.Usuario)
                .Map(x => x.MapKey("IdUsuario"));

            HasMany(x => x.Badges)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("IdUsuario");
                    x.MapRightKey("IdBadge");
                    x.ToTable("BadgeUsuario");
                });
        }
    }
}
