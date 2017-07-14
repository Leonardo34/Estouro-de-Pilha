using EstouroDePilha.Dominio.Entidades;
using System.Collections.Generic;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Usuario ObterPorEmail(string email);
        int QuantidadeDownVotesUsuario(Usuario usuario);
        int QuantidadeUpVotesUsuario(Usuario usuario);
        List<Usuario> ObterUsuariosCadastraosHa(int dias);
    }
}
