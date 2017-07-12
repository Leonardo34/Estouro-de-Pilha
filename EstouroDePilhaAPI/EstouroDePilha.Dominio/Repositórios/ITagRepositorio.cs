using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface ITagRepositorio : IRepositorioGenerico<Tag>
    {
        Dictionary<Tag, int> BuscarTagsUsuarioPorId(int id);

    }
}
