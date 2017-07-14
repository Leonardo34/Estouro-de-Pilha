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

    }
}
