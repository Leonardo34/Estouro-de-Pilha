using System.Collections.Generic;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IPerguntaRepositorio : IRepositorioGenerico<Pergunta>
    {
        List<Pergunta> ObterPerguntasPeloTitulo(string titulo);
        List<Pergunta> Paginacao(string titulo, int quantidadePular);
        int  NumeroDeResultadosDaPesquisa(string titulo);
        List<Pergunta> ObterPerguntasUsuarioPorId(int id);
        List<Pergunta> ObterPerguntas(string busca, string pergunta);

    }
}