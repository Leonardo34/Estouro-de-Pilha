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
using EstouroDePilhaAPI.Models;
using EstouroDePilha.Dominio.Servicos;

namespace EstouroDePilhaAPI.Controllers
{
    [RoutePrefix("api/respostas")]
    public class RespostaController : ControllerBase
    {
        private readonly IRespostaRepositorio respostasRepositorio;
        private readonly IPerguntaRepositorio perguntasRepositorio;
        private readonly IUsuarioRepositorio usuariosRepositorio;
        private readonly IBadgeService badgeService;

        public RespostaController(IRespostaRepositorio respostasRepositorio,
            IPerguntaRepositorio perguntasRepositorio, IUsuarioRepositorio usuariosRepositorio,
            IBadgeService badgeService)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.respostasRepositorio = respostasRepositorio;
            this.usuariosRepositorio = usuariosRepositorio;
            this.badgeService = badgeService;
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
        public HttpResponseMessage AdicionarResposta([FromBody]RespostaModel respostaModel, int idPergunta)
        {
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            var resposta = new Resposta(usuario, pergunta, respostaModel.Descricao);
            if (!resposta.EhValida())
            {
                return ResponderErro(resposta.Mensagens);
            }
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
            try
            {
                resposta.UpVote(usuario);
                respostasRepositorio.Alterar(resposta);
                badgeService.ChecarBadges(usuario);
                badgeService.ChecarBadges(resposta.Usuario);
                return ResponderOK();
            }
            catch (Exception e)
            {
                return ResponderErro(e.Message);
            }
        }

        [BasicAuthorization]
        [HttpPost, Route("{idResposta:int}/downvote")]
        public HttpResponseMessage AdicionarDownvoteResposta(int idResposta)
        {
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            try
            {
                resposta.DownVote(usuario);
                respostasRepositorio.Alterar(resposta);
                return ResponderOK();
            }
            catch (Exception e)
            {
                return ResponderErro(e.Message);
            }
        }

        [BasicAuthorization]
        [HttpPost, Route("{idResposta:int}/comentar")]
        public HttpResponseMessage AdicionarComentario(int idResposta, [FromBody]ComentarioRespostaModel comentarioModel)
        {
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var resposta = respostasRepositorio.ObterPorId(idResposta);
            resposta.Comentar(usuario, comentarioModel.Descricao);
            respostasRepositorio.Alterar(resposta);
            return ResponderOK();
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
            var usuario = usuariosRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = perguntasRepositorio.ObterPorId(resposta.Pergunta.Id);
            if (pergunta.SelecionarRespostaCorreta(resposta, usuario))
            {
                badgeService.ChecarBadges(usuario);
                badgeService.ChecarBadges(resposta.Usuario);
                respostasRepositorio.Alterar(resposta);
                return ResponderOK();
            }
            return ResponderErro("Você não pode marcar esta resposta como correta");
        }

        [HttpGet]
        [Route("numeroDeRespostasDaPergunta/{idPergunta:int}")]
        public HttpResponseMessage NumeroDeResultadosDaPesquisa(int idPergunta)
        {
            int numeroDeRespostasDaPergunta = respostasRepositorio.NumeroDeRespostasPorPergunta(idPergunta);
            bool temRespostaCorreta = respostasRepositorio.VerificaSeTemRespostaCorretaPorIdPergunta(idPergunta);
            return ResponderComOutrosDados(numeroDeRespostasDaPergunta, temRespostaCorreta);
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
            respostaModel.Comentarios = new List<ComentarioRespostaModel>();
            foreach (var each in entidadeResposta.Comentarios)
            {
                var comentario = new ComentarioRespostaModel();
                comentario.Usuario = each.Usuario.converterUsuarioParaUsuarioModel();
                comentario.Id = each.Id;
                comentario.DataComentario = each.DataComentario;
                comentario.Descricao = each.Descricao;
                respostaModel.Comentarios.Add(comentario);
            }
            return respostaModel;
        }
    }
}