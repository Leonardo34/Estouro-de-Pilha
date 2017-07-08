using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IRespostaRepositorio : IRepositorioGenerico<Resposta>
    {
        List<Resposta> ObterRespostasPeloIdPergunta(int id);
        void AdicionarUpvote(UpVoteResposta upvote);
    }
}
