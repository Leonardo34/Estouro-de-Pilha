﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Resposta : EntidadeBase
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public String Descricao { get; private set; }
        public DateTime DataResposta { get; private set; }
        public Pergunta Pergunta { get; private set; }
        public bool? EhRespostaCorreta { get; private set; }
        public List<UpVoteResposta> UpVotes { get; private set; }
        public List<DownVoteResposta> DownVotes { get; private set; }

        protected Resposta()
        {
        }

        public Resposta(Usuario usuario, Pergunta pergunta, string descricao)
        {
            Usuario = usuario;
            Pergunta = pergunta;
            Descricao = descricao;
            EhRespostaCorreta = false;
            DataResposta = DateTime.Now;
            UpVotes = new List<UpVoteResposta>();
            DownVotes = new List<DownVoteResposta>();
        }

        public override bool EhValida()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição é inválido.");

            return Mensagens.Count == 0;
        }

        public bool UsuarioJaInteragiuComResposta(Usuario usuario)
        {
            return UpVotes.Any(u => u.Usuario.Id == usuario.Id)
                || DownVotes.Any(d => d.Usuario.Id == usuario.Id);            
        }

        private bool UsuarioPodeEditar(Usuario usuario)
        {
            //gauderio irá editar quando quiser
            return Usuario.Id == usuario.Id && (EhRespostaCorreta == false || EhRespostaCorreta == null);
        }

        public void Editar(string descricao, Usuario usuario)
        {
            if (UsuarioPodeEditar(usuario))
            {
                Descricao = descricao;
            }
        }

        public void MarcarComoCorreta()
        {
            EhRespostaCorreta = true;
        }
    }
}

