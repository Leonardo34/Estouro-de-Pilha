using System.Collections.Generic;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IPerguntaRepositorio : IRepositorioGenerico<Pergunta>
    {
        List<Pergunta> ObterTodasAsPerguntasDaPesquisa(string conteudoDaBusca, string tags);
        List<Pergunta> Paginacao(int quantidadePular, string conteudoDaBusca, string tags);
        int  NumeroDeResultadosDaPesquisa(string conteudoDaBusca, string tags);
        List<Pergunta> ObterPerguntasUsuarioPorId(int id);
        List<Pergunta> BuscaPerguntasPorTags(string tags);
        List<Pergunta> BuscaPerguntasPorTituloEDescricao(string conteudo);
    }
}