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
    public class RespostaTeste
    {
        [TestMethod]
        public void TestarMetodoEhValidaRespostaSemDescricao()
        {
            String descricao = "  ";
            Resposta resposta = new Resposta();
            resposta.Descricao = descricao;

            Assert.IsFalse(resposta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaRespostaComDescricaoNula()
        {
            Resposta resposta = new Resposta();

            Assert.IsFalse(resposta.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaRespostaValida()
        {
            String descricao = "Tem que usar ng-if!";
            Resposta resposta = new Resposta();
            resposta.Descricao = descricao;

            Assert.IsTrue(resposta.EhValida());
        }

    }
}
