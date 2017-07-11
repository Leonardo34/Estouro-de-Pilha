using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class UpVotePergunta
    {
        public int Id { get; set; }
        public Pergunta Pergunta { get; set; }
        public Usuario Usuario { get; set; }
    }
}
