using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Resposta : EntidadeBase
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public String Descricao { get; set; }
        public DateTime DataResposta { get; set; }
        public Pergunta Pergunta { get; set; }
        public bool? EhRespostaCorreta { get; set; }
        public List<UpVoteResposta> UpVotes { get; set; }
        public List<DownVoteResposta> DownVotes { get; set; }

        public override bool EhValida()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição é inválido.");

            return Mensagens.Count == 0;
        }

        public bool UsuarioJaInteragiuComResposta(Usuario usuario)
        {
            return usuario.Id == Usuario.Id
                || UpVotes.Any(u => u.Usuario.Id == usuario.Id)
                || DownVotes.Any(d => d.Usuario.Id == usuario.Id);            
        }

        public void MarcarComoCorreta()
        {
            this.EhRespostaCorreta = true;
        }
    }
}

