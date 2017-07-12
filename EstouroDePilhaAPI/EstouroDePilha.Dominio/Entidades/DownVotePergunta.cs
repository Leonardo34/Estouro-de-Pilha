using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class DownVotePergunta
    {
        public int Id { get; private set; }
        public virtual Pergunta Pergunta { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public DateTime Data { get; private set; }

        protected DownVotePergunta()
        {
        }

        public DownVotePergunta(Pergunta pergunta, Usuario usuario)
        {
            Pergunta = pergunta;
            Usuario = usuario;
            Data = DateTime.Now;
        }
    }
}
