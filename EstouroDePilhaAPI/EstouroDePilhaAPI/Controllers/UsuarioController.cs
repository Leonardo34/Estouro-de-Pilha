using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Excecoes;
using EstouroDePilha.Dominio.Repositórios;
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
    [RoutePrefix("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio repositorio;

        public UsuarioController (IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        [HttpPost, Route("registrar")]
        public HttpResponseMessage Registrar([FromBody]RegistrarUsuarioModel model)
        {
            var usuario = repositorio.ObterPorEmail(model.Email);
            if (usuario == null)
            {
                usuario = new Usuario(model.Nome, model.Email, model.Senha);
                if (usuario.EhValida())
                {
                    usuario.DataCadastro = DateTime.Now;
                    usuario.UrlFotoPerfil = model.UrlImagemPerfil;
                    usuario.Endereco = model.Endereco;
                    usuario.Descricao = model.Descricao;
                    repositorio.Criar(usuario);
                }
                else
                {
                    return ResponderErro();
                }
            }
            else
            {
                return ResponderErro("Usuário já existe.");
            }

            return ResponderOK(new { usuario.Id });
        }

        [BasicAuthorization]
        [HttpGet]
        public HttpResponseMessage ListarUsuarios()
        {           
            var usuarios = repositorio.Listar();
            return ResponderOK(usuarios);    
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Usuario usuario)
        {
            if (repositorio.ObterPorEmail(usuario.Email) == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }
            repositorio.Deletar(usuario);
            return ResponderOK(usuario);
        }

        [BasicAuthorization]
        [HttpGet, Route("login")]
        public HttpResponseMessage FazerLogin()
        {
            Usuario usuario = repositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);

            return ResponderOK(usuario.converterUsuarioParaUsuarioModel());
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar([FromBody]RegistrarUsuarioModel model)
        {
            var usuario = new Usuario(model.Id, model.Nome,  model.Endereco, model.Senha, model.Descricao, model.UrlImagemPerfil);

            if (repositorio.ObterPorEmail(usuario.Email) == null || !usuario.EhValida())
            {
                throw new Exception();
            }
            return ResponderOK();    
        }
    }
}