using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IPerguntaRepositorio : IRepositorioGenerico<Pergunta>
    {
        List<Pergunta> ObterPerguntasUsuarioPorId(int id);
    }

}