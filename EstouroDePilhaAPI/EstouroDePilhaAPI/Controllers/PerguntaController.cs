using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using EstouroDePilha.Infraestrutura;
using EstouroDePilha.Infraestrutura.Repositórios;
using EstouroDePilhaAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EstouroDePilhaAPI.Controllers
{
    [RoutePrefix("api/perguntas")]
    public class PerguntaController : ControllerBase
    {
        private readonly IPerguntaRepositorio repositorio;
        public PerguntaController(IPerguntaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public HttpResponseMessage ListarPerguntas()
        {
            var perguntas = repositorio.Listar();
            return ResponderOK(perguntas);
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Pergunta pergunta)
        {
            if (repositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            repositorio.Deletar(pergunta);
            return ResponderOK(pergunta);
        }

        [BasicAuthorization]
        [HttpPost]
        public HttpResponseMessage Criar(Pergunta pergunta)
        {
            if (!pergunta.EhValida())
            {
                throw new Exception();
            }
            repositorio.Criar(pergunta);
            return ResponderOK(pergunta);
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar(Pergunta pergunta)
        {
            if (repositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(pergunta);
        }
    }
}