using EstouroDePilha.Dominio.Entidades;
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
            respostasRepositorio.Criar(resposta);
            return ResponderOK();
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar(Resposta resposta)
        {
            if (respostasRepositorio.ObterPorId(resposta.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(resposta);
        }

        [HttpGet, Route("pergunta/{quantidadePular:int}/{idPergunta:int}")]
        public HttpResponseMessage BuscarRespostasPergunta(int quantidadePular, int idPergunta)
        {
            var respostas = respostasRepositorio.PaginacaoRespostas(quantidadePular, idPergunta);
            List<RespostaModel> respostasDto = new List<RespostaModel>();
            foreach (var each in respostas)
            {
                var resposta = new RespostaModel();
                resposta.Id = each.Id;
                resposta.Usuario = each.Usuario.converterUsuarioParaUsuarioModel();
                resposta.Descricao = each.Descricao;
                resposta.DataResposta = each.DataResposta;
                resposta.QuantidadeUpVotes = each.UpVotes.Count;
                resposta.QuantidadeDownVotes = each.DownVotes.Count;
                resposta.EhRespostaCorreta = each.EhRespostaCorreta;
                respostasDto.Add(resposta);
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

        [HttpGet]
        [Route("numeroDeRespostasDaPergunta/{idPergunta:int}")]
        public HttpResponseMessage NumeroDeResultadosDaPesquisa(int idPergunta)
        {
            int NumeroDeRespostasDaPergunta = respostasRepositorio.NumeroDeRespostasPorPergunta(idPergunta);
            return ResponderOK(NumeroDeRespostasDaPergunta);
        }
    }
}