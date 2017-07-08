using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class UpVoteResposta : EntidadeBase
    {
        public int Id { get; set; }
        public Resposta Resposta { get; set; }
        public Usuario Usuario { get; set; }

        public override bool EhValida()
        {
            return Resposta != null && Usuario != null;
        }
    }
}
