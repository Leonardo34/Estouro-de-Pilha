using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Badge
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public virtual List<Usuario> Usuarios { get; private set; }

        public Badge(string titulo, string descricao)
        {
            this.Titulo = titulo;
            this.Descricao = descricao;
        }

        protected Badge()
        {
            
        }
    }
}

