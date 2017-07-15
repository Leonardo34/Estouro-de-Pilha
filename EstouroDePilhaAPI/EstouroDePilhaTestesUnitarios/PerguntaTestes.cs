using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, " ", "Java");

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaSemDescricao()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario,"Java", null);

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaComDescricaoETituloNulos()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, null, null);

            Assert.IsFalse(pergunta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaPerguntaValida()
        {
            Usuario usuario = new Usuario("Leoanardo", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            Assert.IsTrue(pergunta.EhValida());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComRespostasVaziasDeveSerFalso()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComRespostasNaoCorretas()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "java");
            pergunta.Respostas.Add(resposta);

            Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComUmaRespostaCorreta()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "java");
            List<Resposta> respostas = new List<Resposta>();
            resposta.MarcarComoCorreta();
            pergunta.Respostas.Add(resposta);

            Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        }

        [TestMethod]
        public void ExisteRespostaCorretaComApenasUmRespostaPopuladaSendoElaCorreta()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "java");
            List <Resposta> respostas = new List<Resposta>();
            resposta.MarcarComoCorreta();
            pergunta.Respostas.Add(resposta);

            Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        }

        //[TestMethod]
        //public void SelecionarRespostaCorretaApenasUmaRespostaCadastradaComUsuarioCorreto()
        //{
        //    Usuario usuario = new Usuario("Leonardo", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "123");
        //    usuario.Id = 1;
        //    Pergunta pergunta = new Pergunta();
        //    pergunta.Usuario = usuario;
        //    pergunta.Respostas = new List<Resposta>();
        //    Resposta resposta1 = new Resposta();
        //    resposta1.Usuario = new Usuario("Leonardo", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "123"); 
        //    resposta1.EhRespostaCorreta = false;
        //    pergunta.Respostas.Add(resposta1);

        //    Assert.IsTrue(pergunta.SelecionarRespostaCorreta(resposta1));
        //    Assert.IsTrue((bool)resposta1.EhRespostaCorreta);
        //    Assert.IsTrue(pergunta.ExisteRespostaCorreta());
        //}

        //[TestMethod]
        //public void SelecionarRespostaCorretaEntreTresRespostasCadastradasComUsuarioIgual()
        //{
        //    Usuario usuario = new Usuario("Leonardo", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "123");
        //    usuario.Id = 1;
        //    Pergunta pergunta = new Pergunta();
        //    pergunta.Usuario = usuario;
        //    pergunta.Respostas = new List<Resposta>();
        //    Resposta resposta1 = new Resposta();
        //    resposta1.EhRespostaCorreta = false;
        //    resposta1.Usuario = usuario;
        //    pergunta.Respostas.Add(resposta1);

        //    Assert.IsFalse(pergunta.SelecionarRespostaCorreta(resposta1));
        //    Assert.IsFalse((bool)resposta1.EhRespostaCorreta);
        //    Assert.IsFalse(pergunta.ExisteRespostaCorreta());
        //}

        [TestMethod]
        public void EditarPerguntaEmMenosDeSeteDias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, DateTime.Now.AddDays(-6), null);
            }

            pergunta.Editar("teste", "teste titulo", usuario);
            Assert.AreEqual("teste titulo", pergunta.Titulo);
            Assert.AreEqual("teste", pergunta.Descricao);
        }

        [TestMethod]
        public void EditarPerguntaEmMenosDeUmaHoraAntesDeCompletarSeteDias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, DateTime.Now.AddDays(-6).AddHours(23), null);
            }

            pergunta.Editar("teste", "teste titulo", usuario);
            Assert.AreEqual("teste titulo", pergunta.Titulo);
            Assert.AreEqual("teste", pergunta.Descricao);
        }

        [TestMethod]
        public void EditarPerguntaEmMenosDeUmMinutoAntesDeCompletarSeteDias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, DateTime.Now.AddDays(-6).AddHours(-23).AddMinutes(-59), null);
            }

            pergunta.Editar("teste", "teste titulo", usuario);
            Assert.AreEqual("teste titulo", pergunta.Titulo);
            Assert.AreEqual("teste", pergunta.Descricao);
        }

        [TestMethod]
        public void EditarPerguntaEmMenosDeUmSegundooAntesDeCompletarSeteDias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, DateTime.Now.AddDays(-6).AddHours(-23).AddMinutes(-59).AddSeconds(-59), null);
            }

            pergunta.Editar("teste", "teste titulo", usuario);
            Assert.AreEqual("teste titulo", pergunta.Titulo);
            Assert.AreEqual("teste", pergunta.Descricao);
        }

        [TestMethod]
        public void EditarPerguntaEmExatosMenos7Dias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, DateTime.Now.AddDays(-6).AddHours(-22), null);
            }

            pergunta.Editar("teste", "teste titulo", usuario);
            Assert.AreEqual("teste titulo", pergunta.Titulo);
            Assert.AreEqual("teste", pergunta.Descricao);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EditarPerguntaDepoisDeUmSegundoPassado7Dias()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");

            PropertyInfo prop = pergunta.GetType().GetProperty("DataPergunta", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(pergunta, pergunta.DataPergunta.AddDays(-7).AddSeconds(-1), null);
            }

            pergunta.Editar("teste", "teste", usuario);
        }

        [TestMethod]
        public void UpVoteComUsuarioQueAindaNaoInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            pergunta.UpVote(usuario2);

            Assert.AreEqual(pergunta.UpVotes[0].Usuario, usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioJaDeuUpVoteException))]
        public void UpVoteComUsuarioQueJaInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            pergunta.UpVote(usuario2);
            pergunta.UpVote(usuario2);
        }

        [TestMethod]
        public void DownVoteComUsuarioQueAindaNaoInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            pergunta.DownVote(usuario2);

            Assert.AreEqual(pergunta.DownVotes[0].Usuario, usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioJaDeuDownVoteException))]
        public void DownVoteComUsuarioQueJaInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            pergunta.DownVote(usuario2);
            pergunta.DownVote(usuario2);
        }
    }
}
