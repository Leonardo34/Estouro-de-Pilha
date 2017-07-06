using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Tag
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    }
}
