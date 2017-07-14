using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class PerguntaHomeModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public UsuarioPerguntaHomeModel Usuario { get; set; }
        public PerguntaHomeModel(int id, string titulo, int idUsuario, string url, string nomeUsuario, List<Badge> badges)
        {
            Usuario = new UsuarioPerguntaHomeModel(idUsuario, url, nomeUsuario, badges);
            Id = id;
            Titulo = titulo;
        }
    }
}
