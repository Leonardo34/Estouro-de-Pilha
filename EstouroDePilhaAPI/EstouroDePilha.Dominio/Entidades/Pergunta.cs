using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Pergunta
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public List<Resposta> Respostas { get; set; }
        public DateTime DataPergunta { get; set; }
    }
}
