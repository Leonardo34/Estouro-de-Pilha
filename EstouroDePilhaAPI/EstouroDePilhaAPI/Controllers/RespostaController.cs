﻿using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Models;
using EstouroDePilha.Dominio.Excecoes;
using EstouroDePilha.Dominio.Repositórios;
using EstouroDePilha.Infraestrutura;
using EstouroDePilha.Infraestrutura.Repositórios;
using EstouroDePilhaAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using EstouroDePilhaAPI.Models;

namespace EstouroDePilhaAPI.Controllers
{
    [RoutePrefix("api/respostas")]
    public class RespostaController : ControllerBase
    {
        private readonly IRespostaRepositorio respostasRepositorio;
        private readonly IPerguntaRepositorio perguntasRepositorio;
        private readonly IUsuarioRepositorio usuariosRepositorio;

        public RespostaController(IRespostaRepositorio respostasRepositorio,
            IPerguntaRepositorio perguntasRepositorio, IUsuarioRepositorio usuariosRepositorio)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.respostasRepositorio = respostasRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
        }

        [HttpGet, Route()]
        public HttpResponseMessage ListarRespostas()
        {
            var respostas = respostasRepositorio.Listar();
            return ResponderOK(respostas);
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Resposta resposta)
        {
            if (respostasRepositorio.ObterPorId(resposta.Id) == null)
            {
                throw new Exception();
            }
            respostasRepositorio.Deletar(resposta);
            return ResponderOK(resposta);
        }

        [BasicAuthorization]
        [HttpPost, Route("nova/{idPergunta:int}")]
        public HttpResponseMessage AdicionarResposta([FromBody]Resposta resposta, int idPergunta)
        {
            if (!resposta.EhValida())
            {
                throw new Exception();
            }
            resposta.Usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            resposta.Pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            resposta.DataResposta = DateTime.Now;
            resposta.EhRespostaCorreta = false;
            respostasRepositorio.Criar(resposta);
            return ResponderOK();
        }

        [BasicAuthorization]
        [HttpPut, Route("editar/{idResposta:int}")]
        public HttpResponseMessage Alterar([FromBody]RespostaModel respostaModel, int idResposta)
        {
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            if (resposta == null)
            {
                return ResponderErro("Pergunta não existe");
            }
            resposta.Editar(respostaModel.Descricao, usuario);
            respostasRepositorio.Alterar(resposta);
            return ResponderOK(CriarModelResposta(resposta));
        }

        [HttpGet, Route("pergunta/{idPergunta:int}")]
        public HttpResponseMessage BuscarRespostasPergunta(int skip, int idPergunta)
        {
            var respostas = respostasRepositorio.ObterRespostasPaginadas(skip, idPergunta);
            List<RespostaModel> respostasDto = new List<RespostaModel>();
            foreach (var each in respostas)
            {
                respostasDto.Add(CriarModelResposta(each));
            }
            return ResponderOK(respostasDto);
        }


        [BasicAuthorization]
        [HttpPost, Route("{idResposta:int}/upvote")]
        public HttpResponseMessage AdicionarUpvoteResposta(int idResposta)
        {
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            if (resposta.UsuarioJaInteragiuComResposta(usuario))
            {
                return ResponderErro("Você não pode mais dar UpVote nesta resposta");
            }
            var upvote = new UpVoteResposta();
            upvote.Usuario = usuario;
            upvote.Resposta = resposta;
            respostasRepositorio.AdicionarUpvote(upvote);
            return ResponderOK(new { Id = upvote.Id });
        }

        [BasicAuthorization]
        [HttpPost, Route("{idResposta:int}/downvote")]
        public HttpResponseMessage AdicionarDownvoteResposta(int idResposta)
        {
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            if (resposta.UsuarioJaInteragiuComResposta(usuario))
            {
                return ResponderErro("Você não pode mais dar DownVote nesta resposta");
            }
            var downvote = new DownVoteResposta();
            downvote.Usuario = usuario;
            downvote.Resposta = resposta;
            respostasRepositorio.AdicionarDownvote(downvote);
            return ResponderOK(new { Id = downvote.Id });
        }

        [HttpGet, Route("usuario/{id:int}")]
        public HttpResponseMessage ObterRespostasUsuarioPorId(int id)
        {
            var respostasUsuario = respostasRepositorio.ObterRespostasPorUsuarioId(id);
            if (respostasUsuario == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }
            return ResponderOK(respostasUsuario);
        }

        [BasicAuthorization]
        [HttpPut, Route("correta/{idResposta:int}")]
        public HttpResponseMessage SelecionarRespostaCorreta(int idResposta)
        {
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            int idPergunta = resposta.Pergunta.Id;
            var pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            if (pergunta.SelecionarRespostaCorreta(resposta))
            {
                respostasRepositorio.Alterar(resposta);
                return ResponderOK();
            }
            return ResponderErro("Você não pode marcar esta resposta como correta");
        }

        public RespostaModel CriarModelResposta(Resposta entidadeResposta)
        {
            var respostaModel = new RespostaModel();
            respostaModel.Id = entidadeResposta.Id;
            respostaModel.Usuario = entidadeResposta.Usuario.converterUsuarioParaUsuarioModel();
            respostaModel.Descricao = entidadeResposta.Descricao;
            respostaModel.DataResposta = entidadeResposta.DataResposta;
            respostaModel.QuantidadeUpVotes = entidadeResposta.UpVotes.Count;
            respostaModel.QuantidadeDownVotes = entidadeResposta.DownVotes.Count;
            respostaModel.EhRespostaCorreta = entidadeResposta.EhRespostaCorreta;
            respostaModel.DownVotes = new List<UsuarioBaseModel>();
            foreach (var downvote in entidadeResposta.DownVotes)
            {
                respostaModel.DownVotes.Add(downvote.Usuario.converterUsuarioParaUsuarioModel());
            }
            respostaModel.UpVotes = new List<UsuarioBaseModel>();
            foreach (var upvote in entidadeResposta.UpVotes)
            {
                respostaModel.UpVotes.Add(upvote.Usuario.converterUsuarioParaUsuarioModel());
            }
            return respostaModel;
        }

        [HttpGet]
        [Route("numeroDeRespostasDaPergunta/{idPergunta:int}")]
        public HttpResponseMessage NumeroDeResultadosDaPesquisa(int idPergunta)
        {
            int numeroDeRespostasDaPergunta = respostasRepositorio.NumeroDeRespostasPorPergunta(idPergunta);
            bool temRespostaCorreta = respostasRepositorio.VerificaSeTemRespostaCorretaPorIdPergunta(idPergunta);
            return ResponderComOutrosDados(numeroDeRespostasDaPergunta, temRespostaCorreta);
        }
    }
}