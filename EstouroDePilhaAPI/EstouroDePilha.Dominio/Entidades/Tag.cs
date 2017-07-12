using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Tag : EntidadeBase
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual List<Pergunta> Perguntas { get; set; }

        public override bool EhValida()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição é inválido.");

            return Mensagens.Count == 0;
        }
    }
}
