using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class UpVoteResposta : EntidadeBase
    {
        public int Id { get; private set; }
        public virtual Resposta Resposta { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public DateTime Data { get; private set; }

        public UpVoteResposta()
        {
        }

        public UpVoteResposta(Resposta resposta, Usuario usuario)
        {
            Resposta = resposta;
            Usuario = usuario;
            Data = DateTime.Now;
        }

        public override bool EhValida()
        {
            return Resposta != null && Usuario != null;
        }
    }
}
