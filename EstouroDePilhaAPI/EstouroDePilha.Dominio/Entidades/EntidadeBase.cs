using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    internal abstract class EntidadeBase
    {
        List<String> Mensagens { get; set; }

        public abstract bool EhValida();

        public abstract List<String> PegarMensagens();
    }
}
