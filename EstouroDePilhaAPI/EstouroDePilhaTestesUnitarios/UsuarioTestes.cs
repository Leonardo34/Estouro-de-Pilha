using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilhaTestesUnitarios
{
    [TestClass]
    public class UsuarioTestes
    {
        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemNome()
        {
            Usuario usuario = new Usuario("","mateus@gmail.com","123345");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemEmail()
        {
            Usuario usuario = new Usuario("Mateus", "", "123345");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemSenha()
        {
            Usuario usuario = new Usuario("mateus", "mateus@gmail.com", "");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioComCamposObrigatoriosCertos()
        {
            Usuario usuario = new Usuario("mateus", "mateus@gmail.com", "q1223");
            Assert.IsTrue(usuario.EhValida());
        }
    }
}
