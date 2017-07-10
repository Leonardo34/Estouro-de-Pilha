using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class RespostaRepositorio : IRespostaRepositorio
    {
        private readonly Contexto contexto;

        public RespostaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Resposta resposta)
        {
            contexto.Entry(resposta).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Criar(Resposta resposta)
        {
            contexto.Respostas.Add(resposta);
            contexto.SaveChanges();
        }

        public void Deletar(Resposta resposta)
        {
            contexto.Respostas.Remove(resposta);
            contexto.SaveChanges();
        }

        public List<Resposta> Listar()
        {
            return contexto.Respostas
                .Include("Usuario")
                .Include("UpVotes")
                .Include("DownVotes")
                .ToList();
        }

        public Resposta ObterPorId(int id)
        {
            return contexto.Respostas
                .Include("Usuario")
                .Include("UpVotes")
                .Include("DownVotes")
                .Include("Pergunta")
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Resposta> ObterRespostasPaginadas(int quantidadePular, int idPergunta)
        {
            var quantidadeBuscar = 5;
            List<Resposta> respostas = new List<Resposta>();
            var respostaCerta = BuscaRespostaCorretaPorIdDaPergunta(idPergunta);
            if (respostaCerta != null && quantidadePular == 0)
            {
                respostas.Add(respostaCerta);
                quantidadeBuscar = 4;
            }
            var respostasOrdenadasPorVotos = ObterRespostasPeloIdPergunta(idPergunta)
                 .Except(respostas).OrderByDescending(r => (r.UpVotes.Count() - r.DownVotes.Count()) > 0)
                 .Skip(quantidadePular * 5)
                 .Take(quantidadeBuscar).ToList();
            respostas.AddRange(respostasOrdenadasPorVotos);
            return respostas;
        }

        public Resposta BuscaRespostaCorretaPorIdDaPergunta(int idPergunta)
        {
            return ObterRespostasPeloIdPergunta(idPergunta).FirstOrDefault(c => c.EhRespostaCorreta == true);
        }

        public int NumeroDeRespostasPorPergunta(int idPergunta)
        {
            return ObterRespostasPeloIdPergunta(idPergunta).Count();
        }

        public bool VerificaSeTemRespostaCorretaPorIdPergunta(int idPergunta)
        {
            return BuscaRespostaCorretaPorIdDaPergunta(idPergunta) != null;
        }

        public List<Resposta> ObterRespostasPeloIdPergunta(int id)
        {
            return contexto.Respostas
                .Include("Usuario")
                .Include("UpVotes")
                .Include("UpVotes.Usuario")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Where(r => r.Pergunta.Id == id)
                .ToList();
        }

        public void AdicionarUpvote(UpVoteResposta upvote)
        {
            contexto.UpVotesResposta.Add(upvote);
            contexto.SaveChanges();
        }

        public List<Resposta> ObterRespostasPorUsuarioId(int id)
        {
            return contexto.Respostas.Where(p => p.Usuario.Id == id).ToList();
        }

        public void AdicionarDownvote(DownVoteResposta downvote)
        {
            contexto.DownVotesResposta.Add(downvote);
            contexto.SaveChanges();
        }
    }
}

