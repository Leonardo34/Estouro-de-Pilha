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
    [RoutePrefix("api/respostas")]
    public class RespostaController :ControllerBase
    {
        private readonly IRespostaRepositorio repositorio;

        public RespostaController(IRespostaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpGet]
        public HttpResponseMessage ListarRespostas()
        {
            var respostas = repositorio.Listar();
            return ResponderOK(respostas);
        }

        [BasicAuthorization]
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

        [BasicAuthorization]
        [HttpPost]
        public HttpResponseMessage Criar(Resposta resposta)
        {      
            if (!resposta.EhValida())
            {
                throw new Exception();
            }
            repositorio.Criar(resposta);
            return ResponderOK(resposta);
        }

        [BasicAuthorization]
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