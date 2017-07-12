using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class UpVotePergunta
    {
        public int Id { get; private set; }
        public Pergunta Pergunta { get; private set; }
        public Usuario Usuario { get; private set; }

        protected UpVotePergunta()
        {
        }

        public UpVotePergunta(Pergunta pergunta, Usuario usuario)
        {
            Pergunta = pergunta;
            Usuario = usuario;
        }
    }
}
