using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class DownVoteResposta : EntidadeBase
    {
        public int Id { get; private set; }
        public Resposta Resposta { get; private set; }
        public Usuario Usuario { get; private set; }
        public DateTime Data { get; private set; }

        protected DownVoteResposta()
        {
        }

        public DownVoteResposta(Resposta resposta, Usuario usuario)
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
