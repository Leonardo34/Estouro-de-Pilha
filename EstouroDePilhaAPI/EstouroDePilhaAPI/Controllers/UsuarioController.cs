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
                usuario = new Usuario(model.Nome, model.Endereco, model.Descricao, model.UrlImagemPerfil, model.Email, model.Senha);

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
                    return ResponderErro(usuario.Mensagens);
                }
            }
            else
            {
                throw new ExcecaoUsuarioInvalido("Usuario já existe");
            }

            return ResponderOK(new { usuario.Id });
        }

        [BasicAuthorization]
        [HttpGet, Route("")]
        public HttpResponseMessage ListarUsuarios()
        {           
            var usuarios = repositorio.Listar();
            return ResponderOK(usuarios);   
        }

        [BasicAuthorization]
        [HttpDelete, Route("")]
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

            return ResponderLogin(usuario.converterUsuarioParaUsuarioModel());
        }



        [BasicAuthorization]
        [HttpPut, Route("")]
        public HttpResponseMessage Alterar([FromBody]RegistrarUsuarioModel model)
        {
            var usuario = repositorio.ObterPorId(model.Id);
            usuario.Nome = model.Nome;
            usuario.Endereco = model.Endereco;
            usuario.Descricao = model.Descricao;
            usuario.UrlFotoPerfil = model.UrlImagemPerfil;
            repositorio.Alterar(usuario);

            return ResponderOK(new { usuario });    
        }

        [HttpGet, Route("{id:int}")]
        public HttpResponseMessage pegarUsuario(int id)
        {
            Usuario usuario = repositorio.ObterPorId(id);
            if(usuario == null)
            {
                throw new ExcecaoUsuarioNaoExistente();
            }
            return ResponderOK(usuario.converterUsuarioParaUsuarioModel());
        }
    }
}