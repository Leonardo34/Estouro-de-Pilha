using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public interface RepositorioBase<T>
    {
        void Criar(T entity);
        T ObterPorId(int id);
        T Alterar(T entity);
        T Deletar(T entity);
        List<T> Listar();
    }
}