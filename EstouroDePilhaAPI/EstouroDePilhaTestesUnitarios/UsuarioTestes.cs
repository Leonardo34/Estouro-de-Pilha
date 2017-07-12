﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilhaTestesUnitarios
{
    [TestClass]
    public class UsuarioTestes
    {
        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemNome()
        {
            Usuario usuario = new Usuario("", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "123");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemEmail()
        {
            Usuario usuario = new Usuario("Leonardo", "", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "123");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioSemSenha()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "");
            Assert.IsFalse(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoEhValidaUsuarioComCamposObrigatoriosCertos()
        {
            Usuario usuario = new Usuario("Leonardo", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Assert.IsTrue(usuario.EhValida());
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgePapudoRetornoFalso()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta3 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta4 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta5 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta6 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta7 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta8 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta9 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta10 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta11 = new Resposta(usuario1, pergunta, "java");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta1);
            usuario1.Respostas.Add(resposta2);
            usuario1.Respostas.Add(resposta3);
            usuario1.Respostas.Add(resposta4);
            usuario1.Respostas.Add(resposta5);
            usuario1.Respostas.Add(resposta6);
            usuario1.Respostas.Add(resposta7);
            usuario1.Respostas.Add(resposta8);
            usuario1.Respostas.Add(resposta9);
            usuario1.Respostas.Add(resposta10);
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            Badge badgePapudo = new Badge("Papudo", "teste");

            Assert.IsFalse(usuario1.AdicionaBadgePapudo(badgePapudo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgePapudoRetornoTrue()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta3 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta4 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta5 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta6 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta7 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta8 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta9 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta10 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta11 = new Resposta(usuario1, pergunta, "java");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta1);
            usuario1.Respostas.Add(resposta2);
            usuario1.Respostas.Add(resposta3);
            usuario1.Respostas.Add(resposta4);
            usuario1.Respostas.Add(resposta5);
            usuario1.Respostas.Add(resposta6);
            usuario1.Respostas.Add(resposta7);
            usuario1.Respostas.Add(resposta8);
            usuario1.Respostas.Add(resposta9);
            usuario1.Respostas.Add(resposta10);
            usuario1.Respostas.Add(resposta11);
            List<Badge> badges = new List<Badge>();
            Badge badgePapudo = new Badge("Papudo", "teste");
            usuario1.Badges = badges;

            Assert.IsTrue(usuario1.AdicionaBadgePapudo(badgePapudo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgePapudoRetornoFalseUsuarioJáTemBadgePapudo()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta3 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta4 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta5 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta6 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta7 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta8 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta9 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta10 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta11 = new Resposta(usuario1, pergunta, "java");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta1);
            usuario1.Respostas.Add(resposta2);
            usuario1.Respostas.Add(resposta3);
            usuario1.Respostas.Add(resposta4);
            usuario1.Respostas.Add(resposta5);
            usuario1.Respostas.Add(resposta6);
            usuario1.Respostas.Add(resposta7);
            usuario1.Respostas.Add(resposta8);
            usuario1.Respostas.Add(resposta9);
            usuario1.Respostas.Add(resposta10);
            usuario1.Respostas.Add(resposta11);
            List<Badge> badges = new List<Badge>();
            Badge badge = new Badge("Papudo", "teste");
            badges.Add(badge);
            usuario1.Badges = badges;
            Badge badgePapudo = new Badge("Papudo", "teste");

            Assert.IsFalse(usuario1.AdicionaBadgePapudo(badgePapudo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDeVeredaRetornoTrue()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            resposta1.MarcarComoCorreta();
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            List<Resposta> respostas = new List<Resposta>();
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta1);
            Badge badgeDeVereda = new Badge("De vereda", "teste");

            Assert.IsTrue(usuario1.AdicionaBadgeDeVereda(badgeDeVereda, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDeVeredaRetornoFalse()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            resposta2.MarcarComoCorreta();
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            List<Resposta> respostas = new List<Resposta>();
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta1);
            Badge badgeDeVereda = new Badge("De vereda", "teste");

            Assert.IsFalse(usuario1.AdicionaBadgeDeVereda(badgeDeVereda, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDeEntreveroRetornoTrue()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");        
            Resposta resposta3 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta4 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta5 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta6 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta7 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta8 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta9 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta10 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta11 = new Resposta(usuario2, pergunta, "java");
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            pergunta.Respostas.Add(resposta3);
            pergunta.Respostas.Add(resposta4);
            pergunta.Respostas.Add(resposta5);
            pergunta.Respostas.Add(resposta6);
            pergunta.Respostas.Add(resposta7);
            pergunta.Respostas.Add(resposta8);
            pergunta.Respostas.Add(resposta9);
            pergunta.Respostas.Add(resposta10);
            pergunta.Respostas.Add(resposta11);
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            List<Pergunta> perguntas = new List<Pergunta>();
            usuario1.Perguntas = perguntas;
            usuario1.Perguntas.Add(pergunta);
            Badge badgeEntrevero= new Badge("Entrevero", "teste");

            Assert.IsTrue(usuario1.AdicionaBadgeEntrevero(badgeEntrevero, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDeEntreveroRetornoFalse()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta3 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta4 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta5 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta6 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta7 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta8 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta9 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta10 = new Resposta(usuario2, pergunta, "java");
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            pergunta.Respostas.Add(resposta3);
            pergunta.Respostas.Add(resposta4);
            pergunta.Respostas.Add(resposta5);
            pergunta.Respostas.Add(resposta6);
            pergunta.Respostas.Add(resposta7);
            pergunta.Respostas.Add(resposta8);
            pergunta.Respostas.Add(resposta9);
            pergunta.Respostas.Add(resposta10);
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            List<Pergunta> perguntas = new List<Pergunta>();
            usuario1.Perguntas = perguntas;
            usuario1.Perguntas.Add(pergunta);
            Badge badgeEntrevero = new Badge("Entrevero", "teste");

            Assert.IsFalse(usuario1.AdicionaBadgeEntrevero(badgeEntrevero, pergunta.Id));
        }
    }
}
