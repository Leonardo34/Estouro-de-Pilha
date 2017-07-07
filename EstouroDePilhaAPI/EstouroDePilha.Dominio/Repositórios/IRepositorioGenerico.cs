using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IRepositorioGenerico<T>
    {
        void Criar(T entity);
        T ObterPorId(int id);
        void Alterar(T entity);
        void Deletar(T entity);
        List<T> Listar();
    }
}
