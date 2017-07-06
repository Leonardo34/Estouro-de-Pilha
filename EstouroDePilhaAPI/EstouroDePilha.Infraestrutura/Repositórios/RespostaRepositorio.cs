using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class RespostaRepositorio
    {
        private Contexto contexto;

        RespostaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }
    }
}
