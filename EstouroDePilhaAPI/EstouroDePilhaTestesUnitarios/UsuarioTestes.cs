using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;
using System.Collections;

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
        public void TestarMetodoAdicionaBadgeGuriRetornoFalso()
        {
            Usuario usuario = new Usuario("Nao é guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta();
            List <Pergunta > perguntas = new List<Pergunta>();
            List<Resposta> respostas = new List<Resposta>();
            List<Badge> badges  = new List<Badge>();
            usuario.Respostas = respostas;
            usuario.Perguntas = perguntas;
            usuario.Badges = badges;
            
            Assert.IsFalse(usuario.AdicionaBadgeGuri());
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeGuriUsuarioComUmUpVote()
        {
            Usuario usuario = new Usuario("Guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta();
            UpVotePergunta upVote = new UpVotePergunta();
            List<UpVotePergunta> upVotes = new List<UpVotePergunta>();
            upVotes.Add(upVote);
            pergunta.UpVotes = upVotes;
            List<Pergunta> perguntas = new List<Pergunta>();
            perguntas.Add(pergunta);
            List<Resposta> respostas = new List<Resposta>();          
            List<Badge> badges = new List<Badge>();
            usuario.Respostas = respostas;
            usuario.Perguntas = perguntas;
            usuario.Badges = badges;
  
            Assert.IsTrue(usuario.AdicionaBadgeGuri());
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeGuriUsuarioDoisUpVote()
        {
            Usuario usuario = new Usuario("Guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta1 = new Pergunta();
            Pergunta pergunta2 = new Pergunta();
            UpVotePergunta upVote = new UpVotePergunta();
            List<UpVotePergunta> upVotes = new List<UpVotePergunta>();
            upVotes.Add(upVote);
            pergunta1.UpVotes = upVotes;
            pergunta2.UpVotes = upVotes;
            List<Pergunta> perguntas = new List<Pergunta>();
            perguntas.Add(pergunta1);
            perguntas.Add(pergunta2);
            List<Resposta> respostas = new List<Resposta>();
            List<Badge> badges = new List<Badge>();
            usuario.Respostas = respostas;
            usuario.Perguntas = perguntas;
            usuario.Badges = badges;

            Assert.IsFalse(usuario.AdicionaBadgeGuri());
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeTramposoUsuarioEhTramposo()
        {
            Usuario usuario = new Usuario("Guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta();
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta();
            List<Resposta> respostas = new List<Resposta>();
           
            resposta.Usuario = usuario;
            resposta.EhRespostaCorreta = true;
            respostas.Add(resposta);
            pergunta.Usuario = usuario;
            pergunta.Respostas = respostas;
            perguntas.Add(pergunta);
            resposta.Pergunta = pergunta;
            List<Badge> badges = new List<Badge>();
            usuario.Perguntas = perguntas;
            usuario.Badges = badges;

            Assert.IsTrue(usuario.AdicionaBadgeTramposo());
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeTramposoUsuarioNaoEhTramposo()
        {
            Usuario usuario1 = new Usuario("Bagual", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("Guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            usuario1.Id = 1;
            usuario2.Id = 2;
            Pergunta pergunta = new Pergunta();
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta();
            List<Resposta> respostas = new List<Resposta>();
            resposta.Usuario = usuario1;
            resposta.EhRespostaCorreta = true;
            respostas.Add(resposta);
            pergunta.Usuario = usuario2;
            pergunta.Respostas = respostas;
            perguntas.Add(pergunta);
            resposta.Pergunta = pergunta;
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;

            Assert.IsFalse(usuario1.AdicionaBadgeTramposo());
        }
    }
}
