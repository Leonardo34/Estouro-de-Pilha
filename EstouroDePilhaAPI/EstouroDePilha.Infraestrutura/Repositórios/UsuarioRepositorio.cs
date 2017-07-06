using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>
    {
        private Contexto contexto;

        public UsuarioRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Usuario usuario)
        {
            contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Criar(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }

        public void Deletar(Usuario usuario)
        {
            contexto.Usuarios.Remove(usuario);
            contexto.SaveChanges();
        }

        public List<Usuario> Listar()
        {
            return contexto.Usuarios.ToList();
        }

        public Usuario ObterPorId(int id)
        {
            return contexto.Usuarios.FirstOrDefault(u => u.Id == id);
        }

        public Usuario ObterPorEmail(string email)
        {
            return contexto.Usuarios
                .FirstOrDefault(u => u.Email.Equals(email));
        }
    }
}
