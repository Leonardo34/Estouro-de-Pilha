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
    }
}
