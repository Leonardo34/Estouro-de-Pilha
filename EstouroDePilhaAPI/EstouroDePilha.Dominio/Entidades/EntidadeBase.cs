using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public abstract class EntidadeBase
    {
        public List<String> Mensagens { get; set; }

        public EntidadeBase ()
        {
            Mensagens = new List<String>();
        }

        public abstract bool EhValida();
    }
}
