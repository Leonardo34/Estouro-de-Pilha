using EstouroDePilha.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Pergunta : EntidadeBase
    {
        public int Id { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public virtual List<Resposta> Respostas { get; private set; }
        public DateTime DataPergunta { get; private set; }
        public virtual List<Tag> Tags { get; private set; }
        public virtual List<UpVotePergunta> UpVotes { get; private set; }
        public virtual List<DownVotePergunta> DownVotes { get; private set; }
        public virtual List<ComentarioPergunta> ComentariosPergunta { get; set; }

        protected Pergunta()
        {
        }

        public Pergunta(Usuario usuario, string titulo, string descricao)
        {
            Usuario = usuario;
            Titulo = titulo;
            Descricao = descricao;
            DataPergunta = DateTime.Now;
            Respostas = new List<Resposta>();
            Tags = new List<Tag>();
            UpVotes = new List<UpVotePergunta>();
            DownVotes = new List<DownVotePergunta>();
        }

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

        public bool SelecionarRespostaCorreta(Resposta resposta, Usuario usuario)
        {
            if (ExisteRespostaCorreta() || !ExisteRespostaComId(resposta.Id))
            {
                return false;
            }
            resposta.MarcarComoCorreta();
            return true;
        }

        public bool ExisteRespostaComId(int id)
        {
            return Respostas.Any(r => r.Id == id);
        }

        private bool PodeEditar(Usuario usuario)
        {
            return ((DateTime.Now - this.DataPergunta).TotalDays <= 7 
                && usuario.Id == Usuario.Id ) || usuario.Badges.Any(b => b.Titulo.Contains("Gaudério"));
        }

        public bool UsuarioJaInteragiuComPergunta(Usuario usuario)
        {
            return UpVotes.Any(u => u.Usuario.Id == usuario.Id)
                || DownVotes.Any(d => d.Usuario.Id == usuario.Id);
        }

        public bool DeveSerExcluida()
        {
            return (UpVotes.Count() - DownVotes.Count()) <= 4;
        }

        public void AdicionarTag(Tag tag)
        {
            if (!Tags.Any(t => t.Id == tag.Id))
            {
                Tags.Add(tag);
            }
        }

        public void Editar(string descricao, string titulo, Usuario usuario)
        {
            if (PodeEditar(usuario))
            {
                Descricao = descricao;
                Titulo = titulo;
            }
            else
            {
                throw new Exception("Voce não pode editar essa pergunta");
            }
        }

        public void UpVote(Usuario usuario)
        {
            if (UsuarioJaInteragiuComPergunta(usuario))
            {
                throw new UsuarioJaDeuUpVoteException();
            }
            UpVotes.Add(new UpVotePergunta(this, usuario));
        }

        public void DownVote(Usuario usuario)
        {
            if (UsuarioJaInteragiuComPergunta(usuario))
            {
                throw new UsuarioJaDeuDownVoteException();
            }
            DownVotes.Add(new DownVotePergunta(this, usuario));
        }

        public void Comentar(Usuario usuario, string descricao)
        {
            ComentariosPergunta.Add(new ComentarioPergunta(this, usuario, descricao));
        }
    }
}

