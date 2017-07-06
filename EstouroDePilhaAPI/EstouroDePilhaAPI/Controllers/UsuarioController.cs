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
    [RoutePrefix("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepositorio repositorio = new UsuarioRepositorio(Contexto.contexto);

        [HttpGet]
        public HttpResponseMessage ListarUsuarios()
        {
            var usuarios = repositorio.Listar();
            return ResponderOK(usuarios);
        }

        [HttpDelete]
        public HttpResponseMessage Deletar(Usuario usuario)
        {
            if (repositorio.ObterPorId(usuario.Id) == null)
            {
                return ResponderErro("Tchê, o usuário não existe!");
            }
            repositorio.Deletar(usuario);
            return ResponderOK(usuario);
        }

        [HttpPost]
        public HttpResponseMessage Criar(Usuario usuario)
        {
            repositorio.Criar(usuario);
            return ResponderOK(usuario);
        }

        [HttpPut]
        public HttpResponseMessage Alterar(Usuario usuario)
        {
            if (repositorio.ObterPorId(usuario.Id) == null)
            {
                return ResponderErro("Tchê, o usuário não existe!");
            }
            repositorio.Alterar(usuario);
            return ResponderOK(usuario);
        }

    }
}