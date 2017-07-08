using EstouroDePilha.Dominio.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilhaTestesUnitarios
{
    [TestClass]
    public class PerguntaTestes
    {
        [TestMethod]
        public void TestarMetodoEhValidaPerguntaSemTitulo()
        {
            String titulo = "Java e Angular";
            Pergunta pergunta = new Pergunta();
            pergunta.Titulo = titulo;

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaSemDescricao()
        {
            String descricao = "Me ajuda no Angular!";
            Pergunta pergunta = new Pergunta();
            pergunta.Descricao = descricao;

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaComDescricaoETituloNulos()
        {
            Pergunta pergunta = new Pergunta();

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaValida()
        {
            String titulo = "Java e Angular";
            String descricao = "Me ajuda no Angular!";
            Pergunta pergunta = new Pergunta();
            pergunta.Titulo = titulo;
            pergunta.Descricao = descricao;

            Assert.IsTrue(pergunta.EhValida());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComRespostasVaziasDeveSerFalso()
        {
            Pergunta pergunta = new Pergunta();
            pergunta.Respostas = new List<Resposta>();

            Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComRespostasNaoCorretas()
        {
            Pergunta pergunta = new Pergunta();
            pergunta.Respostas = new List<Resposta>();
            Resposta resposta1 = new Resposta();
            resposta1.EhRespostaCorreta = false;
            Resposta resposta2 = new Resposta();
            resposta2.EhRespostaCorreta = false;
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);

            Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComUmaRespostaCorreta()
        {
            Pergunta pergunta = new Pergunta();
            pergunta.Respostas = new List<Resposta>();
            Resposta resposta1 = new Resposta();
            resposta1.EhRespostaCorreta = false;
            Resposta resposta2 = new Resposta();
            resposta2.EhRespostaCorreta = true;
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);

            Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComApenasUmRespostaPopuladaSendoElaCorreta()
        {
            Pergunta pergunta = new Pergunta();
            pergunta.Respostas = new List<Resposta>();
            Resposta resposta1 = new Resposta();
            resposta1.EhRespostaCorreta = true;
            pergunta.Respostas.Add(resposta1);

            Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void SelecionarRespostaCorretaApenasUmaRespostaCadastradaComUsuarioCorreto()
        {
            Usuario usuario = new Usuario("Leonardo", "teste@hotmail.com", "123");
            usuario.Id = 1;
            Pergunta pergunta = new Pergunta();
            pergunta.Usuario = usuario;
            pergunta.Respostas = new List<Resposta>();
            Resposta resposta1 = new Resposta();
            resposta1.Usuario = new Usuario("Leonardo", "teste@hotmail.com", "123"); 
            resposta1.EhRespostaCorreta = false;
            pergunta.Respostas.Add(resposta1);

            Assert.IsTrue(pergunta.SelecionarRespostaCorreta(resposta1));
            Assert.IsTrue((bool)resposta1.EhRespostaCorreta);
            Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void SelecionarRespostaCorretaEntreTresRespostasCadastradasComUsuarioIgual()
        {
            Usuario usuario = new Usuario("Leonardo", "teste@hotmail.com", "123");
            usuario.Id = 1;
            Pergunta pergunta = new Pergunta();
            pergunta.Usuario = usuario;
            pergunta.Respostas = new List<Resposta>();
            Resposta resposta1 = new Resposta();
            resposta1.EhRespostaCorreta = false;
            resposta1.Usuario = usuario;
            pergunta.Respostas.Add(resposta1);

            Assert.IsFalse(pergunta.SelecionarRespostaCorreta(resposta1));
            Assert.IsFalse((bool)resposta1.EhRespostaCorreta);
            Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        }
    }
}
