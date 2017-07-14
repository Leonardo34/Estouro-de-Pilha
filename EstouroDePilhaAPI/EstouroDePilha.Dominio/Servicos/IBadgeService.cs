using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Servicos
{
    public interface IBadgeService
    {
        void UsuarioRecebeuUpVoteResposta(Usuario usuario, int idPergunta);
        void UsuarioDeuUpVote(Usuario usuario);
        void UsuarioMarcouRespostaCorreta(Usuario usuario, int idPergunta);
        void UsuarioRecebeuResposta(Usuario usuario, int idPergunta);
        void UsuarioFezPergunta(Usuario usuario);
        void UsuarioDeuDownVote(Usuario usuario);
        void UsuarioRecebeuDownVote(Usuario usuario);
        void UsuarioRecebeuUpVotePergunta(Usuario usuario, int idPergunta);
        void UsuarioSeCadastrouHaUmAno(Usuario usuario);
        void UsuarioSeCadastrouHaTresAnos(Usuario usuario);
    }
}