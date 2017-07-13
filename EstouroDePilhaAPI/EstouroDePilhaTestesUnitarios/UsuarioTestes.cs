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
            Badge badgeEntrevero = new Badge("Entrevero", "teste");

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

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDePeleador()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            resposta1.MarcarComoCorreta();
            UpVoteResposta upVote = new UpVoteResposta(resposta2, usuario2);
            var contador = 0;
            while (contador <= 11)
            {
                resposta2.UpVotes.Add(upVote);
                contador++;
            }
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);

            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            List<Pergunta> perguntas = new List<Pergunta>();
            usuario1.Perguntas = perguntas;
            usuario1.Perguntas.Add(pergunta);
            Badge badgeEntrevero = new Badge("Peleador", "teste");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta2);
            Badge badgeDeVereda = new Badge("De vereda", "teste");

            Assert.IsTrue(usuario1.AdicionarBadgePeleador(badgeEntrevero, pergunta.Id));
            Assert.IsNotNull(usuario1.Badges[0]);
        }

        [TestMethod]
        public void TestarMetodoAdicionaDuasBadgeDePeleador()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            Resposta resposta3 = new Resposta(usuario2, pergunta, "java");
            resposta1.MarcarComoCorreta();
            UpVoteResposta upVote = new UpVoteResposta(resposta2, usuario2);
            var contador = 0;
            while (contador <= 11)
            {
                resposta2.UpVotes.Add(upVote);
                resposta3.UpVotes.Add(upVote);
                contador++;
            }
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            List<Pergunta> perguntas = new List<Pergunta>();
            usuario1.Perguntas = perguntas;
            usuario1.Perguntas.Add(pergunta);
            Badge badgeEntrevero = new Badge("Peleador", "teste");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta2);
            usuario1.Respostas.Add(resposta3);
            Badge badgeDeVereda = new Badge("De vereda", "teste");

            Assert.IsTrue(usuario1.AdicionarBadgePeleador(badgeEntrevero, pergunta.Id));
            Assert.IsNotNull(usuario1.Badges[0]);
            Assert.IsNotNull(usuario1.Badges[1]);
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeDePeleadorRetornaFalso()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario2, "Java", "me ajuda");
            Resposta resposta1 = new Resposta(usuario1, pergunta, "java");
            Resposta resposta2 = new Resposta(usuario2, pergunta, "java");
            resposta1.MarcarComoCorreta();
            UpVoteResposta upVote = new UpVoteResposta(resposta2, usuario2);
            var contador = 0;
            while (contador < 10)
            {
                resposta2.UpVotes.Add(upVote);
                contador++;
            }
            pergunta.Respostas.Add(resposta1);
            pergunta.Respostas.Add(resposta2);
            List<Badge> badges = new List<Badge>();
            usuario1.Badges = badges;
            List<Pergunta> perguntas = new List<Pergunta>();
            usuario1.Perguntas = perguntas;
            usuario1.Perguntas.Add(pergunta);
            Badge badgeEntrevero = new Badge("Peleador", "teste");
            List<Resposta> respostas = new List<Resposta>();
            usuario1.Respostas = respostas;
            usuario1.Respostas.Add(resposta2);
            Badge badgeDeVereda = new Badge("De vereda", "teste");

            Assert.IsFalse(usuario1.AdicionarBadgePeleador(badgeEntrevero, pergunta.Id));
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

        [TestMethod]
        public void TestarMetodoAdicionarBadgeBaitaPerguntaRetornoTrue()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            UpVotePergunta upVote = new UpVotePergunta(pergunta, usuario2);
            var contador = 1;
            while (contador < 17)
            {
                pergunta.UpVotes.Add(upVote);
                contador++;
            }
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;

            Badge badge= new Badge("Baita pergunta", "teste");
            Assert.IsTrue(usuario1.AdicionarBadgeBaitaPergunta(badge, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionarBadgeBaitaPerguntaRetornoFalse()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            UpVotePergunta upVote = new UpVotePergunta(pergunta, usuario2);
            var contador = 1;
            while (contador < 20)
            {
                pergunta.UpVotes.Add(upVote);
                contador++;
            }
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;

            Badge badge = new Badge("Baita pergunta", "teste");
            Assert.IsFalse(usuario1.AdicionarBadgeBaitaPergunta(badge, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionarBadgeBaitaPerguntaRetornoFalsePerguntaNaoTemVotosSuficiente()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            UpVotePergunta upVote = new UpVotePergunta(pergunta, usuario2);
            var contador = 1;
            while (contador < 10)
            {
                pergunta.UpVotes.Add(upVote);
                contador++;
            }
            perguntas.Add(pergunta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;

            Badge badge = new Badge("Baita pergunta", "teste");
            Assert.IsFalse(usuario1.AdicionarBadgeBaitaPergunta(badge, pergunta.Id));
        }

        [TestMethod]
        public void TestarMetodoAdicionarBadgeGauderiorRetornoFalso()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta(usuario1, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            UpVoteResposta upVoteResposta = new UpVoteResposta(resposta, usuario2);
            UpVotePergunta upVotePergunta = new UpVotePergunta(pergunta, usuario2);
            var contador = 0;
            while (contador < 9)
            {
                pergunta.UpVotes.Add(upVotePergunta);
                resposta.UpVotes.Add(upVoteResposta);
                contador++;
            }
            perguntas.Add(pergunta);
            respostas.Add(resposta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;
            usuario1.Respostas = respostas;
            Badge badge = new Badge("Gaudério", "teste");
            Assert.IsFalse(usuario1.AdicionarBadgeGauderio(badge));
        }

        [TestMethod]
        public void TestarMetodoAdicionarBadgeGauderiorRetornoTrue()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta(usuario1, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            UpVoteResposta upVoteResposta = new UpVoteResposta(resposta, usuario2);
            UpVotePergunta upVotePergunta = new UpVotePergunta(pergunta, usuario2);
            var contador = 0;
            while (contador <= 10)
            {
                pergunta.UpVotes.Add(upVotePergunta);
                resposta.UpVotes.Add(upVoteResposta);
                contador++;
            }
            perguntas.Add(pergunta);
            respostas.Add(resposta);
            List<Badge> badges = new List<Badge>();
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;
            usuario1.Respostas = respostas;
            Badge badge = new Badge("Gaudério", "teste");
            Assert.IsTrue(usuario1.AdicionarBadgeGauderio(badge));
        }

        [TestMethod]
        public void TestarMetodoAdicionarBadgeGauderiorRetornoFalsoUsuarioJahtemABadge()
        {
            Usuario usuario1 = new Usuario("teste 1", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Usuario usuario2 = new Usuario("teste 2", "Rua Sei La", "Qualquer descrição", "https/foto.png", "teste@hotmail.com", "q1223");
            Pergunta pergunta = new Pergunta(usuario1, "Java", "me ajuda");
            List<Pergunta> perguntas = new List<Pergunta>();
            Resposta resposta = new Resposta(usuario1, pergunta, "me ajuda");
            List<Resposta> respostas = new List<Resposta>();
            UpVoteResposta upVoteResposta = new UpVoteResposta(resposta, usuario2);
            UpVotePergunta upVotePergunta = new UpVotePergunta(pergunta, usuario2);
            var contador = 0;
            while (contador <= 10)
            {
                pergunta.UpVotes.Add(upVotePergunta);
                resposta.UpVotes.Add(upVoteResposta);
                contador++;
            }
            perguntas.Add(pergunta);
            respostas.Add(resposta);
            List<Badge> badges = new List<Badge>();
            Badge badge = new Badge("Gaudério", "teste");
            badges.Add(badge);
            usuario1.Perguntas = perguntas;
            usuario1.Badges = badges;
            usuario1.Respostas = respostas;
          
            Assert.IsFalse(usuario1.AdicionarBadgeGauderio(badge));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeAmargoQuandoUsuarioTem6DownVotes()
        {
            Usuario usuario = new Usuario("a", "a", "a", "a", "a", "a");
            usuario.Badges = new List<Badge>();
            usuario.Respostas = new List<Resposta>();
            usuario.Perguntas = new List<Pergunta>();
            Badge amargo = new Badge("Amargo", "Usuário que já deu mais de 5 downvotes");
            usuario.AdicionaBadgeAmargo(amargo, 6);

            Assert.IsTrue(usuario.Badges.Contains(amargo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeAmargoQuandoUsuarioTemMenosDe5DownVotes()
        {
            Usuario usuario = new Usuario("a", "a", "a", "a", "a", "a");
            usuario.Badges = new List<Badge>();
            usuario.Respostas = new List<Resposta>();
            usuario.Perguntas = new List<Pergunta>();
            Badge amargo = new Badge("Amargo", "Usuário que já deu mais de 5 downvotes");
            usuario.AdicionaBadgeAmargo(amargo, 4);

            Assert.IsFalse(usuario.Badges.Contains(amargo));
        }

        [TestMethod]
        public void TestarMetodoAdicionaBadgeAmargoNaoDuplicaABadge()
        {
            Usuario usuario = new Usuario("a", "a", "a", "a", "a", "a");
            usuario.Badges = new List<Badge>();
            usuario.Respostas = new List<Resposta>();
            usuario.Perguntas = new List<Pergunta>();
            Badge amargo = new Badge("Amargo", "Usuário que já deu mais de 5 downvotes");
            usuario.AdicionaBadgeAmargo(amargo, 6);

            Assert.IsFalse(usuario.AdicionaBadgeAmargo(amargo, 7));
        }
    }
}
