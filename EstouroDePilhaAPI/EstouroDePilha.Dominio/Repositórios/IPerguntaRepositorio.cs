using System.Collections.Generic;
using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Models;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IPerguntaRepositorio : IRepositorioGenerico<Pergunta>
    {
        List<Pergunta> ObterTodasAsPerguntasDaPesquisa(string conteudoDaBusca, string tags);
        List<Pergunta> ObterResultadosDaBuscaPaginados(int quantidadePular, string conteudoDaBusca, string tags);
        int  NumeroDeResultadosDaPesquisa(string conteudoDaBusca, string tags);
        List<PerguntaPerfilModel> ObterTop5PerguntasUsuarioPorId(int id);
        List<Pergunta> ListarPaginado(int skip, int take);
        List<Pergunta> BuscaPerguntasPorTags(string tags);
        List<Pergunta> BuscaPerguntasPorTituloEDescricao(string conteudo);
        int TotalPerguntasCadastradas();
        List<Pergunta> RetornarPerguntasOrdenadasPorMaiorNumeroDeUpVotes(List<Pergunta> perguntas);
        void AdicionarUpvote(UpVotePergunta upvote);
        void AdicionarDownvote(DownVotePergunta downvote);
        List<Pergunta> ObterTop5PerguntasMaiorNumUpvotes();
    }
}