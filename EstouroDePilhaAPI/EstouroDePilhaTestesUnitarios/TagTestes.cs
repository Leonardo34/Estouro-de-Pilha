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
    public class TagTestes
    {
        [TestMethod]
        public void TestarMetodoEhValidaTagSemDescricao()
        {
            String guriDeApartamento = " ";
            Tag tag = new Tag();
            tag.Descricao = guriDeApartamento;

            Assert.IsFalse(tag.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaTagComDescricaoNula()
        {
            Tag tag = new Tag();

            Assert.IsFalse(tag.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaTagComDescricaoValida()
        {
            String guriDeApartamento = "Guri de Apartamento";
            Tag tag = new Tag();
            tag.Descricao = guriDeApartamento;

            Assert.IsTrue(tag.EhValida());
        }
    }
}
