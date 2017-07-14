using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Excecoes;
using EstouroDePilha.Dominio.Models;
using EstouroDePilha.Dominio.Repositórios;
using EstouroDePilha.Dominio.Servicos;
using EstouroDePilha.Infraestrutura;
using EstouroDePilha.Infraestrutura.Repositórios;
using EstouroDePilhaAPI.App_Start;
using EstouroDePilhaAPI.Models;
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
        private readonly IBadgeService badgeService;

        public PerguntaController(IPerguntaRepositorio perguntasRepositorio,
                IUsuarioRepositorio usuarioRepositorio, ITagRepositorio tagsRepositorio,
                IBadgeService badgeService)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.tagsRepositorio = tagsRepositorio;
            this.badgeService = badgeService;
        }

        [HttpGet, Route()]
        public HttpResponseMessage ListarPerguntas(int skip, int take)
        {
            var perguntas = perguntasRepositorio.ListarPaginado(skip, take);
            var perguntasDto = CriarPerguntasDtoHome(perguntas);
            return ResponderOK(perguntasDto);
        }

        [HttpGet, Route("total")]
        public HttpResponseMessage TotalDePerguntasCadastradas()
        {
            return ResponderOK(perguntasRepositorio.TotalPerguntasCadastradas());
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
        public HttpResponseMessage Criar([FromBody]PerguntaModel perguntaModel)
        {
            var usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = CriarEntidadePergunta(perguntaModel, usuario);
            if (!pergunta.EhValida())
            {
                return ResponderErro(pergunta.Mensagens);
            }
            perguntasRepositorio.Criar(pergunta);
            badgeService.UsuarioFezPergunta(usuario);
            return ResponderOK(new { id = pergunta.Id });
        }

        [BasicAuthorization]
        [HttpPost]
        [Route("{idPergunta:int}/upvote")]
        public HttpResponseMessage AdicionarUpvotePergunta(int idPergunta)
        {
            var usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            try
            {
                pergunta.UpVote(usuario);
                perguntasRepositorio.Alterar(pergunta);
                badgeService.UsuarioRecebeuUpVotePergunta(pergunta.Usuario, pergunta.Id);
                badgeService.UsuarioDeuUpVote(usuario);
                return ResponderOK();
            }
            catch (UsuarioJaDeuUpVoteException e)
            {
                return ResponderErro(e.Message);
            }
        }

        [BasicAuthorization]
        [HttpPost]
        [Route("{idPergunta:int}/downvote")]
        public HttpResponseMessage AdicionarDownVotePergunta(int idPergunta)
        {
            var usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            try
            {
                pergunta.DownVote(usuario);
                perguntasRepositorio.Alterar(pergunta);
                badgeService.UsuarioDeuDownVote(usuario);
                badgeService.UsuarioRecebeuDownVote(pergunta.Usuario);
                return ResponderOK();
            }
            catch (UsuarioJaDeuDownVoteException e)
            {
                return ResponderErro(e.Message);
            }
        }

        [BasicAuthorization]
        [HttpPost, Route("{idPergunta:int}/comentar")]
        public HttpResponseMessage AdicionarComentario(int idPergunta, [FromBody]ComentarioRespostaModel comentarioModel)
        {
            var usuario = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var pergunta = perguntasRepositorio.ObterPorId(idPergunta);
            pergunta.Comentar(usuario, comentarioModel.Descricao);
            perguntasRepositorio.Alterar(pergunta);
            return ResponderOK();
        }

        [BasicAuthorization]
        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage Alterar([FromBody]PerguntaModel perguntaModel)
        {
            var usuarioLogado = usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            var perguntaBuscada = perguntasRepositorio.ObterPorId(perguntaModel.Id);
            try
            {
                perguntaBuscada.Editar(perguntaModel.Descricao, perguntaModel.Titulo, usuarioLogado);
                perguntasRepositorio.Alterar(perguntaBuscada);
                return ResponderOK();
            }
            catch (Exception e)
            {
                return ResponderErro(e.Message);
            }
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
        [Route("numeroDeResultadosDaBusca")]
        public HttpResponseMessage NumeroDeResultadosDaPesquisa(string conteudo, string tags)
        {
            int NumeroDeResultadosDaPesquisa = perguntasRepositorio.NumeroDeResultadosDaPesquisa(conteudo, tags);
            return ResponderOK(NumeroDeResultadosDaPesquisa);
        }

        [HttpGet]
        [Route("usuario/{id:int}")]
        public HttpResponseMessage ObterPerguntasPorUsuarioId(int id)
        {
            var perguntasUsuario = perguntasRepositorio.ObterTop5PerguntasUsuarioPorId(id);
            
            if (perguntasUsuario == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }
            return ResponderOK(perguntasUsuario);
        }

        [HttpGet]
        [Route("pesquisa")]
        public HttpResponseMessage ObterResultadosDaBuscaPaginados(int skip, string conteudo, string tags)
        {
            var perguntasPaginadas = perguntasRepositorio.ObterResultadosDaBuscaPaginados(skip, conteudo, tags);
            var perguntasDto = CriarPerguntasDto(perguntasPaginadas);
            return ResponderOK(perguntasDto);
        }

        [HttpGet]
        [Route("top")]
        public HttpResponseMessage ObterTop5PerguntasNumUpvote()
        {
            var topPerguntas = perguntasRepositorio.ObterTop5PerguntasMaiorNumUpvotes();
            var topPerguntasDto = CriarPerguntaTop5Dto(topPerguntas);
            return ResponderOK(topPerguntasDto);
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
            perguntaModel.DownVotes = new List<UsuarioBaseModel>();
            perguntaModel.UpVotes = new List<UsuarioBaseModel>();
            var tags = entidadePergunta.Tags;
            foreach (var tag in tags)
            {
                var tagModel = new TagModel();
                tagModel.Id = tag.Id;
                tagModel.Descricao = tag.Descricao;
                perguntaModel.Tags.Add(tagModel);
            }
            perguntaModel.Comentarios = new List<ComentarioRespostaModel>();
            foreach (var each in entidadePergunta.ComentariosPergunta)
            {
                var comentario = new ComentarioRespostaModel();
                comentario.Usuario = each.Usuario.converterUsuarioParaUsuarioModel();
                comentario.Id = each.Id;
                comentario.DataComentario = each.DataComentario;
                comentario.Descricao = each.Descricao;
                perguntaModel.Comentarios.Add(comentario);
            }
            perguntaModel.QuantidadeDownVotes = entidadePergunta.DownVotes.Count();
            perguntaModel.QuantidadeUpVotes = entidadePergunta.UpVotes.Count();
            foreach (var each in entidadePergunta.UpVotes)
            {
                perguntaModel.UpVotes.Add(each.Usuario.converterUsuarioParaUsuarioModel());
            }
            foreach (var each in entidadePergunta.DownVotes)
            {
                perguntaModel.DownVotes.Add(each.Usuario.converterUsuarioParaUsuarioModel());
            }
            return perguntaModel;
        }

        private List<PerguntaTop5UpvoteModel> CriarPerguntaTop5Dto(List<Pergunta> perguntas)
        {
            List<PerguntaTop5UpvoteModel> perguntasDto = new List<PerguntaTop5UpvoteModel>();
            perguntas.ForEach(p => perguntasDto.Add(new PerguntaTop5UpvoteModel(p.Id, p.Titulo)));
            return perguntasDto;
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

        private List<PerguntaHomeModel> CriarPerguntasDtoHome(List<Pergunta> pergunta)
        {
            List<PerguntaHomeModel> perguntasDto = new List<PerguntaHomeModel>();
            pergunta.ForEach(p => perguntasDto.Add(
                new PerguntaHomeModel
                (p.Id, p.Titulo, p.Usuario.Id, 
                p.Usuario.UrlFotoPerfil, p.Usuario.Nome, p.Usuario.Badges)));
            return perguntasDto;
        }

        private Pergunta CriarEntidadePergunta(PerguntaModel perguntaModel, Usuario usuario)
        {
            var pergunta = new Pergunta(usuario, perguntaModel.Titulo, perguntaModel.Descricao);
            if (perguntaModel.TagsIds != null)
            {
                perguntaModel.TagsIds
                    .ForEach(tag => pergunta.AdicionarTag(tagsRepositorio.ObterPorId(tag)));
            }
            return pergunta;
        }
    }
}