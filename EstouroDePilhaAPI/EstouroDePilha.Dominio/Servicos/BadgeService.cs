﻿using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Servicos
{
    public class BadgeService : IBadgeService
    {
        private readonly IPerguntaRepositorio perguntaRepositorio;
        private readonly IUsuarioRepositorio usuarioRepositorio;
        private readonly ITagRepositorio tagRepositorio;
        private readonly IRespostaRepositorio respostaRepositorio;
        private readonly IBadgeRepositorio badgeRepositorio;

        private static readonly int ID_BADGE_GURI = 7;
        private static readonly int ID_BADGE_TRAMPOSO = 12;
        private static readonly int ID_BADGE_PELEADOR = 14;
        private static readonly int ID_BADGE_ENTREVERO = 15;
        private static readonly int ID_BADGE_DE_VEREDA = 16;
        private static readonly int ID_BADGE_PAPUDO = 17;
        private static readonly int ID_BADGE_AMARGO = 9;

        public BadgeService(IPerguntaRepositorio perguntaRepositorio, 
                IUsuarioRepositorio usuarioRepositorio, 
                ITagRepositorio tagRepositorio, 
                IRespostaRepositorio respostaRepositorio,
                IBadgeRepositorio badgeRepositorio)
        {
            this.perguntaRepositorio = perguntaRepositorio;
            this.usuarioRepositorio = usuarioRepositorio;
            this.tagRepositorio = tagRepositorio;
            this.respostaRepositorio = respostaRepositorio;
            this.badgeRepositorio = badgeRepositorio;
        }

        public void UsuarioRecebeuUpVoteResposta(Usuario usuario, int idPergunta)
        {
            Badge badgeGuri = badgeRepositorio.ObterPorId(ID_BADGE_GURI);
            usuario.AdicionaBadgeGuri(badgeGuri);
            Badge badgePeleador = badgeRepositorio.ObterPorId(ID_BADGE_PELEADOR);
            usuario.AdicionarBadgePeleador(badgePeleador, idPergunta);

            usuarioRepositorio.Alterar(usuario);
        }

        public void UsuarioDeuUpVote(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void UsuarioDeuDownVote(Usuario usuario)
        {
            Badge badgeAmargo = badgeRepositorio.ObterPorId(ID_BADGE_AMARGO);
            int quantidadeDownVotes = usuarioRepositorio.QuantidadeDownVotesUsuario(usuario);
            usuario.AdicionaBadgeAmargo(badgeAmargo, quantidadeDownVotes);
        }

        public void UsuarioMarcouRespostaCorreta(Usuario usuario, int idPergunta)
        {
            Badge badgeTramposo = badgeRepositorio.ObterPorId(ID_BADGE_TRAMPOSO);
            Badge badgeDeVereda = badgeRepositorio.ObterPorId(ID_BADGE_DE_VEREDA);
            usuario.AdicionaBadgeDeVereda(badgeDeVereda, idPergunta);
            usuario.AdicionaBadgeTramposo(badgeTramposo);

            usuarioRepositorio.Alterar(usuario);
        }

        public void UsuarioRecebeuResposta(Usuario usuario, int idPergunta)
        {
            Badge badgeEntrevero = badgeRepositorio.ObterPorId(ID_BADGE_ENTREVERO);
            usuario.AdicionaBadgeEntrevero(badgeEntrevero, idPergunta);

            usuarioRepositorio.Alterar(usuario);
        }

        public void UsuarioFezPergunta(Usuario usuario)
        {
            Badge badgePapudo = badgeRepositorio.ObterPorId(ID_BADGE_PAPUDO);
            usuario.AdicionaBadgePapudo(badgePapudo);

            usuarioRepositorio.Alterar(usuario);
        }
    }
}