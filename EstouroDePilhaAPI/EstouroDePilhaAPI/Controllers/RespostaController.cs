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
    [RoutePrefix("api/respostas")]
    public class RespostaController :ControllerBase
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

        [HttpGet, Route("pergunta/{idPergunta:int}")]
        public HttpResponseMessage BuscarRespostasPergunta(int idPergunta)
        {
            var respostas = respostasRepositorio.ObterRespostasPeloIdPergunta(idPergunta);
            List<RespostaModel> respostasDto = new List<RespostaModel>();
            foreach (var each in respostas)
            {
                var resposta = new RespostaModel();
                resposta.Id = each.Id;
                resposta.Usuario = each.Usuario.converterUsuarioParaUsuarioModel();
                resposta.Descricao = each.Descricao;
                resposta.DataResposta = each.DataResposta;
                respostasDto.Add(resposta);
            }
            return ResponderOK(respostasDto);
        }
    }
}