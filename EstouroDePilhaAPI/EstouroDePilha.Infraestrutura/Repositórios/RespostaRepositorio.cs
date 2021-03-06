﻿using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Models;
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
                .Include("Usuario.Perguntas")
                .Include("Usuario.Respostas")
                .Include("Usuario.Respostas.Usuario")
                .Include("Usuario.Perguntas.Usuario")
                .Include("Usuario.Badges")
                .Include("UpVotes")
                .Include("Upvotes.Usuario")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Include("Pergunta")
                .Include("Comentarios")
                .Include("Comentarios.Usuario")
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
                 .Except(respostas).OrderByDescending(r => (r.UpVotes.Count() - r.DownVotes.Count()))
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
                .Include("Comentarios")
                .Include("Comentarios.Usuario")
                .Where(r => r.Pergunta.Id == id)
                .ToList();
        }

        public void AdicionarUpvote(UpVoteResposta upvote)
        {
            contexto.UpVotesResposta.Add(upvote);
            contexto.SaveChanges();
        }

        public List<RespostaPerfilModel> ObterTop5RespostasPorUsuarioId(int id)
        {
            List<RespostaPerfilModel> respostasModel = new List<RespostaPerfilModel>();
            var respostas = contexto.Respostas
                .Where(r => r.Usuario.Id == id)
                .OrderByDescending(r => r.DataResposta)
                .Take(5)
                .Select(r => new { r.Descricao, r.Pergunta.Id, r.Pergunta.Titulo })
                .ToList();
            respostas.ForEach(r => respostasModel.Add(new RespostaPerfilModel(r.Id, r.Titulo, r.Descricao)));
            return respostasModel;
        }

        public void AdicionarDownvote(DownVoteResposta downvote)
        {
            contexto.DownVotesResposta.Add(downvote);
            contexto.SaveChanges();
        }

        public void AdicionarComentario(ComentarioResposta comentario)
        {
            contexto.ComentariosRespostas.Add(comentario);
            contexto.SaveChanges();
        }
    }
}

