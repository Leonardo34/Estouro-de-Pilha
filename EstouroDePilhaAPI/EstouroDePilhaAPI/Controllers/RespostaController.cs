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
    [RoutePrefix("api/respostas")]
    public class RespostaController :ControllerBase
    {
        private RespostaRepositorio repositorio = new RespostaRepositorio(Contexto.contexto);

        [HttpGet]
        public HttpResponseMessage ListarRespostas()
        {
            var respostas = repositorio.Listar();
            return ResponderOK(respostas);
        }

        [HttpDelete]
        public HttpResponseMessage Deletar(Resposta resposta)
        {
            if (repositorio.ObterPorId(resposta.Id) == null)
            {
                throw new Exception();
            }
            repositorio.Deletar(resposta);
            return ResponderOK(resposta);
        }

        [HttpPost]
        public HttpResponseMessage Criar(Resposta resposta)
        {      
            repositorio.Criar(resposta);
            return ResponderOK(resposta);
        }

        [HttpPut]
        public HttpResponseMessage Alterar(Resposta resposta)
        {
            if (repositorio.ObterPorId(resposta.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(resposta);
        }
    }
}