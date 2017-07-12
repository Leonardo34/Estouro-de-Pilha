using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IRespostaRepositorio : IRepositorioGenerico<Resposta>
    {
        List<Resposta> ObterRespostasPeloIdPergunta(int id);
        void AdicionarUpvote(UpVoteResposta upvote);
        List<Resposta> ObterRespostasPorUsuarioId (int id);
        void AdicionarDownvote(DownVoteResposta downvote);
        List<Resposta> ObterRespostasPaginadas(int quantidadePular, int idPergunta);
        int NumeroDeRespostasPorPergunta(int idPergunta);
        bool VerificaSeTemRespostaCorretaPorIdPergunta(int idPergunta);
        void AdicionarComentario(ComentarioResposta comentario);
    }
}
