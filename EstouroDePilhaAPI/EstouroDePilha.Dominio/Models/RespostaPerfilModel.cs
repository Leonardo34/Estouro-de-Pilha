using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class RespostaPerfilModel
    {
        public int IdPergunta { get; set; }
        public string TituloPergunta { get; set; }
        public string Descricao { get; set; }

        public RespostaPerfilModel(int idPergunta, string tituloPergunta, string descricao)
        {
            IdPergunta = idPergunta;
            TituloPergunta = tituloPergunta;
            Descricao = descricao;
        }
    }
}
