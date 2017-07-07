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
    [RoutePrefix ("api/tags")]
    public class TagController :ControllerBase 
    {
        private readonly ITagRepositorio repositorio;

        public TagController (ITagRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }


        [BasicAuthorization]
        [HttpGet]
        public HttpResponseMessage ListarTags()
        {
            var tags = repositorio.Listar();
            return ResponderOK(tags);
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Tag tag)
        {
            if (repositorio.ObterPorId(tag.Id) == null)
            {
                throw new Exception();
            }
            repositorio.Deletar(tag);
            return ResponderOK(tag);
        }

        [BasicAuthorization]
        [HttpPost]
        public HttpResponseMessage Criar(Tag tag)
        {
            if (!tag.EhValida())
            {
                throw new Exception();
            }
            repositorio.Criar(tag);
            return ResponderOK(tag);
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar(Tag tag)
        {
            if (repositorio.ObterPorId(tag.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(tag);
        }
    }
}