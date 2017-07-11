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
        private readonly ITagRepositorio tagsRepositorio;

        public PerguntaController(IPerguntaRepositorio perguntasRepositorio,
                IUsuarioRepositorio usuarioRepositorio, ITagRepositorio tagsRepositorio)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.tagsRepositorio = tagsRepositorio;
        }

        [HttpGet, Route()]
        public HttpResponseMessage ListarPerguntas(int skip, int take)
        {
            var perguntas = perguntasRepositorio.ListarPaginado(skip, take);
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
            var pergunta = CriarEntidadePergunta(perguntaModel);
            pergunta.DataPergunta = DateTime.Now;
            perguntasRepositorio.Criar(pergunta);
            return ResponderOK(new { id = pergunta.Id });
        }

        [BasicAuthorization]
        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage Alterar([FromBody]PerguntaModel perguntaModel)
        {
            var  usuarioLogado =  Thread.CurrentPrincipal.Identity.Name;
            var perguntaBuscada = perguntasRepositorio.ObterPorId(perguntaModel.Id);
            if (perguntaBuscada == null || (usuarioLogado != perguntaBuscada.Usuario.Email))
            {
                throw new Exception();
            }
            var pergunta = CriarEntidadePergunta(perguntaModel);
            pergunta.DataPergunta = perguntaBuscada.DataPergunta;
            pergunta.Id = perguntaModel.Id;
            if (!pergunta.PodeEditar())
            {
                throw new Exception();
            }
            perguntasRepositorio.Alterar(pergunta);
            return ResponderOK();
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage ObterPorId(int id)
        {
            var pergunta = perguntasRepositorio.ObterPorId(id);
            var perguntaModel = CriarModelPergunta(pergunta);
            return ResponderOK(perguntaModel);
        }

        [HttpGet]
        [Route("numeroDeResultadosDaBusca/{conteudo}/{tags}")]
        public HttpResponseMessage NumeroDeResultadosDaPesquisa(string conteudo, string tags)
        {
            int NumeroDeResultadosDaPesquisa = perguntasRepositorio.NumeroDeResultadosDaPesquisa(conteudo, tags);
            return ResponderOK(NumeroDeResultadosDaPesquisa);
        }

        [HttpGet]
        [Route("usuario/{id:int}")]
        public HttpResponseMessage ObterPerguntasPorUsuarioId(int id)
        {
            var perguntasUsuario = perguntasRepositorio.ObterPerguntasUsuarioPorId(id);
            if (perguntasUsuario == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }
            return ResponderOK(perguntasUsuario);
        }

        [HttpGet]
        [Route("pesquisa/{quantidadePular:int}/{conteudo}/{tags}")]
        public HttpResponseMessage ObterResultadosDaBuscaPaginados(int quantidadePular, string conteudo, string tags)
        {
            var perguntasPaginadas = perguntasRepositorio.ObterResultadosDaBuscaPaginados(quantidadePular, conteudo, tags);
            var perguntasDto = CriarPerguntasDto(perguntasPaginadas);
            return ResponderOK(perguntasDto);
        }

        private PerguntaModel CriarModelPergunta(Pergunta entidadePergunta)
        {
            var perguntaModel = new PerguntaModel();
            perguntaModel.Id = entidadePergunta.Id;
            perguntaModel.Titulo = entidadePergunta.Titulo;
            perguntaModel.Usuario = entidadePergunta.Usuario.converterUsuarioParaUsuarioModel();
            perguntaModel.Descricao = entidadePergunta.Descricao;
            perguntaModel.DataPergunta = entidadePergunta.DataPergunta;
            perguntaModel.Tags = new List<TagModel>();
            var tags = entidadePergunta.Tags;
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    var tagModel = new TagModel();
                    tagModel.Id = tag.Id;
                    tagModel.Descricao = tag.Descricao;
                    perguntaModel.Tags.Add(tagModel);
                }
            }
            return perguntaModel;
        }

        private List<PerguntaModel> CriarPerguntasDto(List<Pergunta> perguntas)
        {
            List<PerguntaModel> perguntasDto = new List<PerguntaModel>();
            foreach (var each in perguntas)
            {
                perguntasDto.Add(CriarModelPergunta(each));
            }
            return perguntasDto;
        }

        private Pergunta CriarEntidadePergunta(PerguntaModel perguntaModel)
        {
            var pergunta = new Pergunta();
            pergunta.Tags = new List<Tag>();
            pergunta.Usuario =
               usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            pergunta.Titulo = perguntaModel.Titulo;
            pergunta.Descricao = perguntaModel.Descricao;
            if (perguntaModel.TagsIds != null)
            {

                perguntaModel.TagsIds
                    .ForEach(tag => pergunta.Tags.Add(
                            tagsRepositorio.ObterPorId(tag)
                        )
                    );
            }
            if (!pergunta.EhValida())
            {
                throw new Exception();
            }
            return pergunta;
        }
    }
}