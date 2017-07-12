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
        public Resposta Resposta { get; private set; }
        public Usuario Usuario { get; private set; }

        public UpVoteResposta()
        {
        }

        public UpVoteResposta(Resposta resposta, Usuario usuario)
        {
            Resposta = resposta;
            Usuario = usuario;
        }

        public override bool EhValida()
        {
            return Resposta != null && Usuario != null;
        }
    }
}
