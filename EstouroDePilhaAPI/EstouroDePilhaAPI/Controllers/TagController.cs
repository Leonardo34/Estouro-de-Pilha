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
    [RoutePrefix ("api/tags")]
    public class TagController :ControllerBase 
    {
        private TagRepositorio repositorio = new TagRepositorio(Contexto.contexto);

        [HttpGet]
        public HttpResponseMessage ListarTags()
        {
            var tags = repositorio.Listar();
            return ResponderOK(tags);
        }

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

        [HttpPost]
        public HttpResponseMessage Criar(Tag tag)
        {
            repositorio.Criar(tag);
            return ResponderOK(tag);
        }

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