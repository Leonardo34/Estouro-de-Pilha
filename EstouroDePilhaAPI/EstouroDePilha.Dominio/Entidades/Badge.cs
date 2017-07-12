using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Badge
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public List<Usuario> Usuarios { get; set; }
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

