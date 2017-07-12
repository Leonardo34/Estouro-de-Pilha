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
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            List<Resposta> respostas = new List<Resposta>();
            List<Badge> badges = new List<Badge>();
            usuario.Respostas = respostas;
            usuario.Perguntas = perguntas;
            usuario.Badges = badges;
            Badge badgeGuri = new Badge("Guri", "teste");
            Assert.IsFalse(usuario.AdicionaBadgeGuri(badgeGuri));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeGuriUsuarioComUmUpVote()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario1, pergunta, "me ajuda");
            UpVotePergunta upVote = new UpVotePergunta(pergunta, usuario2);
            pergunta.UpVotes.Add(upVote);
            List<Pergunta> perguntas = new List<Pergunta>();
            List<Resposta> respostas = new List<Resposta>();
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Respostas = respostas;
            usuario1.Badges = badges;
            Badge badgeGuri = new Badge("Guri", "teste");
            Assert.IsTrue(usuario1.AdicionaBadgeGuri(badgeGuri));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeGuriUsuarioJahPossuiABadge()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            Resposta resposta = new Resposta(usuario1, pergunta, "me ajuda");
            UpVotePergunta upVote = new UpVotePergunta(pergunta, usuario2);
            pergunta.UpVotes.Add(upVote);
            List<Pergunta> perguntas = new List<Pergunta>();
            List<Resposta> respostas = new List<Resposta>();
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            Badge badge = new Badge("Guri", "teste");
            badges.Add(badge);
            usuario1.Perguntas = perguntas;
            usuario1.Respostas = respostas;
            usuario1.Badges = badges;
            Badge badgeGuri = new Badge("Guri", "teste");

            Assert.IsFalse(usuario1.AdicionaBadgeGuri(badgeGuri));
        }


        [TestMethod]
        public void TestarMetodoAdicionaBadgeGuriUsuarioDoisUpVote()
        {
            Usuario usuario = new Usuario("Guri", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta1 = new Pergunta(usuario, "Java", "me ajuda");
            Pergunta pergunta2 = new Pergunta(usuario, "Java", "me ajuda");
            UpVotePergunta upVote1 = new UpVotePergunta(pergunta1, usuario);
            UpVotePergunta upVote2 = new UpVotePergunta(pergunta2, usuario);
            pergunta1.UpVotes.Add(upVote1);
            pergunta2.UpVotes.Add(upVote2);

            List<Pergunta> perguntas = new List<Pergunta>();
            List<Resposta> respostas = new List<Resposta>();
            perguntas.Add(pergunta1);
            perguntas.Add(pergunta2);
            List<Badge> badges = new List<Badge>();
            usuario.Perguntas = perguntas;
            usuario.Respostas = respostas;
            usuario.Badges = badges;
            Badge badgeGuri = new Badge("Guri", "teste");


            Assert.IsFalse(usuario.AdicionaBadgeGuri(badgeGuri));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeTramposoUsuarioEhTramposo()
        {
            Usuario usuario = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta(usuario, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            resposta.MarcarComoCorreta();
            respostas.Add(resposta);
            pergunta.Respostas.Add(resposta);
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario.Perguntas = perguntas;
            usuario.Respostas = respostas;
            usuario.Badges = badges;
            Badge badgeTramposo = new Badge("Tramposo", "teste");

            Assert.IsTrue(usuario.AdicionaBadgeTramposo(badgeTramposo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeTramposoUsuarioNaoEhTramposo()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta1 = new Resposta(usuario1, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            Resposta resposta2 = new Resposta(usuario2, pergunta, "me ajuda");
            resposta2.MarcarComoCorreta();
            respostas.Add(resposta1);
            respostas.Add(resposta2);
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Respostas = respostas;
            usuario1.Badges = badges;
            Badge badgeTramposo = new Badge("Tramposo", "teste");

            Assert.IsTrue(usuario1.AdicionaBadgeTramposo(badgeTramposo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeTramposoUsuarioJahTemABadgeEhTramposo()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta1 = new Resposta(usuario1, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            Resposta resposta2 = new Resposta(usuario2, pergunta, "me ajuda");
            resposta2.MarcarComoCorreta();
            respostas.Add(resposta1);
            respostas.Add(resposta2);
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            Badge badge = new Badge("Tramposo", "teste");
            badges.Add(badge);
            usuario1.Perguntas = perguntas;
            usuario1.Respostas = respostas;
            usuario1.Badges = badges;

            Badge badgeTramposo = new Badge("Tramposo", "teste");
            Assert.IsFalse(usuario1.AdicionaBadgeTramposo(badgeTramposo));
        }
    }
}
