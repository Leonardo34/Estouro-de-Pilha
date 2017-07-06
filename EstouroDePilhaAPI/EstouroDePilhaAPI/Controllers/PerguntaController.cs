using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Infraestrutura;
using EstouroDePilha.Infraestrutura.Repositórios;
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
        private PerguntaRepositorio repositorio = new PerguntaRepositorio(Contexto.contexto);

        [HttpGet]
        public HttpResponseMessage ListarPerguntas()
        {
            var perguntas = repositorio.Listar();
            return ResponderOK(perguntas);
        }

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

        [HttpPost]
        public HttpResponseMessage Criar(Pergunta pergunta)
        {
            repositorio.Criar(pergunta);
            return ResponderOK(pergunta);
        }

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