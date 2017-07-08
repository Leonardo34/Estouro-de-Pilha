using System.Collections.Generic;
using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IPerguntaRepositorio : IRepositorioGenerico<Pergunta>
    {
        List<Pergunta> ObterPerguntasPeloTitulo(string titulo);
        List<Pergunta> Paginacao(int quantidadePular);
    }
}