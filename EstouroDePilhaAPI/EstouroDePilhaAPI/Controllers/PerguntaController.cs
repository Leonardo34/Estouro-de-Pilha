using EstouroDePilha.Dominio.Entidades;
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
        private readonly ITagRepositorio tagsRepositorio;

        public PerguntaController(IPerguntaRepositorio perguntasRepositorio,
                IUsuarioRepositorio usuarioRepositorio, ITagRepositorio tagsRepositorio)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.tagsRepositorio = tagsRepositorio;
        }

        [HttpGet, Route()]
        public HttpResponseMessage ListarPerguntas()
        {
            var perguntas = perguntasRepositorio.Listar();
            var perguntasDto = CriarPerguntasDto(perguntas);
            return ResponderOK(perguntasDto);
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
            pergunta.Tags = new List<Tag>();
            pergunta.Usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            pergunta.DataPergunta = DateTime.Now;
            pergunta.Titulo = perguntaModel.Titulo;
            pergunta.Descricao = perguntaModel.Descricao;
            foreach (var each in perguntaModel.TagsIds)
            {
                pergunta.Tags.Add(tagsRepositorio.ObterPorId(each));
            }
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
            perguntaModel.Tags = new List<TagModel>();
            foreach (var each in pergunta.Tags)
            {
                var tagModel = new TagModel();
                tagModel.Id = each.Id;
                tagModel.Descricao = each.Descricao;
                perguntaModel.Tags.Add(tagModel);
            }
            perguntaModel.Id = pergunta.Id;
            perguntaModel.Titulo = pergunta.Titulo;
            perguntaModel.Descricao = pergunta.Descricao;
            perguntaModel.Usuario = pergunta.Usuario.converterUsuarioParaUsuarioModel();
            return ResponderOK(perguntaModel);
        }

        [HttpGet]
        [Route("pesquisa/{titulo}")]
        public HttpResponseMessage ObterPerguntasPeloTitulo(string titulo)
        {
            var perguntasPorTitulo = perguntasRepositorio.ObterPerguntasPeloTitulo(titulo);
            var perguntasDto = CriarPerguntasDto(perguntasPorTitulo);
            return ResponderOK(perguntasDto);
        }

        [HttpGet]
        [Route("paginacaoPesquisa/{titulo}/{quantidadePular:int}")]
        public HttpResponseMessage PaginacaoDePerguntas(string titulo, int quantidadePular)
        {
            var perguntasPaginadas = perguntasRepositorio.Paginacao(titulo, quantidadePular);
            var perguntasDto = CriarPerguntasDto(perguntasPaginadas);
            return ResponderOK(perguntasDto);
        }

        private List<PerguntaModel> CriarPerguntasDto(List<Pergunta> perguntas)
        {
            List<PerguntaModel> perguntasDto = new List<PerguntaModel>();
            foreach (var each in perguntas)
            {
                var perguntaModel = new PerguntaModel();
                perguntaModel.Id = each.Id;
                perguntaModel.Titulo = each.Titulo;
                perguntaModel.Usuario = each.Usuario.converterUsuarioParaUsuarioModel();
                perguntaModel.Descricao = each.Descricao;
                perguntaModel.DataPergunta = each.DataPergunta;
                perguntasDto.Add(perguntaModel);
            }
            return perguntasDto;
        }
    }
}