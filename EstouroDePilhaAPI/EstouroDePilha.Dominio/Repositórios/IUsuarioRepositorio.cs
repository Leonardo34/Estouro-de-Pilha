using EstouroDePilha.Dominio.Entidades;

namespace EstouroDePilha.Dominio.Repositórios
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Usuario ObterPorEmail(string email);
        int QuantidadeDownVotesUsuario(Usuario usuario);
    }
}
