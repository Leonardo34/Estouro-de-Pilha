using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Excecoes;
using EstouroDePilha.Dominio.Models;
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
    [RoutePrefix("api/perguntas")]
    public class PerguntaController : ControllerBase
    {
        private readonly IPerguntaRepositorio perguntasRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public PerguntaController(IPerguntaRepositorio perguntasRepositorio,
                IUsuarioRepositorio usuarioRepositorio)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        [BasicAuthorization]
        [HttpGet]
        [Route("")]
        public HttpResponseMessage ListarPerguntas()
        {
            var perguntas = perguntasRepositorio.Listar();
            return ResponderOK(perguntas);
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Pergunta pergunta)
        {
            if (perguntasRepositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            perguntasRepositorio.Deletar(pergunta);
            return ResponderOK(pergunta);
        }

        [BasicAuthorization]
        [HttpPost]
        [Route("nova")]
        public HttpResponseMessage Criar(PerguntaModel perguntaModel)
        {
            var pergunta = new Pergunta();
            pergunta.Usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            pergunta.DataPergunta = DateTime.Now;
            pergunta.Titulo = perguntaModel.Titulo;
            pergunta.Descricao = perguntaModel.Descricao;
            if (!pergunta.EhValida())
            {
                throw new Exception();
            }
            perguntasRepositorio.Criar(pergunta);
            return ResponderOK(new { id = pergunta.Id });
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar(Pergunta pergunta)
        {
            if (perguntasRepositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(pergunta);
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage ObterPorId(int id)
        {
            var pergunta = perguntasRepositorio.ObterPorId(id);
            var perguntaModel = new PerguntaModel();
            perguntaModel.Id = pergunta.Id;
            perguntaModel.Titulo = pergunta.Titulo;
            perguntaModel.Descricao = pergunta.Descricao;
            perguntaModel.Usuario = pergunta.Usuario.converterUsuarioParaUsuarioModel();
            return ResponderOK(perguntaModel);
        }

        [HttpGet]
        [Route("/usuario/{id:int}")]
        public HttpResponseMessage ObterPerguntasPorUsuarioId (int id)
        {
            var perguntasUsuario = perguntasRepositorio.ObterPerguntasUsuarioPorId(id);

            if(perguntasUsuario == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }

            return ResponderOK(perguntasUsuario);
        }
    }
}