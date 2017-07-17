using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilhaTestesUnitarios
{
    [TestClass]
    public class RespostaTestes
    {
        [TestMethod]
        public void TestarMetodoEhValidaRespostaSemDescricao()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "     ");

            Assert.IsFalse(resposta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaRespostaComDescricaoNula()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, null);

            Assert.IsFalse(resposta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaRespostaValida()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "java");

            Assert.IsTrue(resposta.EhValida());
        }

        [TestMethod]
        public void UpVoteComUsuarioQueAindaNaoInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "Java");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            resposta.UpVote(usuario2);

            Assert.AreEqual(resposta.UpVotes[0].Usuario, usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioJaDeuUpVoteException))]
        public void UpVoteComUsuarioQueJaInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "Java");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            resposta.UpVote(usuario2);
            resposta.UpVote(usuario2);
        }

        [TestMethod]
        public void DownVoteComUsuarioQueAindaNaoInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "Java");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            resposta.DownVote(usuario2);

            Assert.AreEqual(resposta.DownVotes[0].Usuario, usuario2);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioJaDeuDownVoteException))]
        public void DownVoteComUsuarioQueJaInteragiu()
        {
            Usuario usuario = new Usuario("Mateus", "Rua Mario Bandeira", "Costs aren't losses ", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario, pergunta, "Java");
            Usuario usuario2 = new Usuario("Leonardo", "Sad", "Costs", "https/foto.png", "teste@hotmail.com", "q");
            resposta.DownVote(usuario2);
            resposta.DownVote(usuario2);
        }
    }
}
