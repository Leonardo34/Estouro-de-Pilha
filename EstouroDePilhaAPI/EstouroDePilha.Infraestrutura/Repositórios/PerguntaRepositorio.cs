using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class PerguntaRepositorio
    {
        private Contexto contexto;

        public PerguntaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }
    }
}
