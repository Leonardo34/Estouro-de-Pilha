using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Pergunta : EntidadeBase
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public List<Resposta> Respostas { get; set; }
        public DateTime DataPergunta { get; set; }
        public List<Tag> Tags { get; set; }

        public override bool EhValida()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Titulo))
                Mensagens.Add("Título é inválido.");

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição é inválido.");

            return Mensagens.Count == 0;
        }

        public bool ExisteRespostaCorreta()
        {
            return Respostas.Any(r => r.EhRespostaCorreta == true);
        }

        public bool SelecionarRespostaCorreta(Resposta resposta)
        {
            if (ExisteRespostaCorreta()
                    || !ExisteRespostaComId(resposta.Id)
                    || Usuario.Id == resposta.Usuario.Id)
            {
                return false;
            }
            resposta.EhRespostaCorreta = true;
            return true;
        }

        public bool ExisteRespostaComId(int id)
        {
            return Respostas.Any(r => r.Id == id);
        }
    }
}
