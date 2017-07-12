using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class BadgeRepositorio : IBadgeRepositorio
    {
        private readonly Contexto contexto;

        public BadgeRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public Badge ObterPorId(int id)
        {
            return contexto.Badges
                .FirstOrDefault(b => b.Id == id);
        }
    }
}