﻿using EstouroDePilha.Dominio.Entidades;
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
    [RoutePrefix("api/perguntas")]
    public class PerguntaController : ControllerBase
    {
    
        private readonly IPerguntaRepositorio perguntasRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        public PerguntaController(IPerguntaRepositorio perguntasRepositorio, 
                IUsuarioRepositorio usuarioRepositorio)
        {
            this.perguntasRepositorio = perguntasRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
        }


        [BasicAuthorization]
        [HttpGet]
        public HttpResponseMessage ListarPerguntas()
        {
            var perguntas = perguntasRepositorio.Listar();
            return ResponderOK(perguntas);
        }

        [BasicAuthorization]
        [HttpDelete]
        public HttpResponseMessage Deletar(Pergunta pergunta)
        {
            if (perguntasRepositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            perguntasRepositorio.Deletar(pergunta);
            return ResponderOK(pergunta);
        }

        [BasicAuthorization]
        [HttpPost]
        [Route("nova")]
        public HttpResponseMessage Criar(Pergunta pergunta)
        {

            pergunta.Usuario = 
                usuarioRepositorio.ObterPorEmail(Thread.CurrentPrincipal.Identity.Name);
            perguntasRepositorio.Criar(pergunta);

            if (!pergunta.EhValida())
            {
                throw new Exception();
            }
            perguntasRepositorio.Criar(pergunta);

            return ResponderOK(pergunta);
        }

        [BasicAuthorization]
        [HttpPut]
        public HttpResponseMessage Alterar(Pergunta pergunta)
        {
            if (perguntasRepositorio.ObterPorId(pergunta.Id) == null)
            {
                throw new Exception();
            }
            return ResponderOK(pergunta);
        }

        [HttpGet]
        [Route("{id:int}")]
        [BasicAuthorization]
        public HttpResponseMessage ObterPorId(int id)
        {
            var pergunta = perguntasRepositorio.ObterPorId(id);
            return ResponderOK(new { descricao = pergunta.Descricao, idUsuario = pergunta.Usuario.Id });
        }
    }
}