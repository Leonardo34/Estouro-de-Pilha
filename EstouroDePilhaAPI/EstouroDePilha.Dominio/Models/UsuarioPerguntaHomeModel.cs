using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class UsuarioPerguntaHomeModel
    {
        public int Id { get; set; }
        public string UrlImagemPerfil { get; set; }
        public string Nome { get; set; }
        public List<BadgeUsuarioHomeModel> Badges { get; set; } //string pq quero só o tipo da badge
 
        public UsuarioPerguntaHomeModel(int id, string url, string nome, List<Badge> badges)
        {
            Badges = new List<BadgeUsuarioHomeModel>();
            Id = id;
            UrlImagemPerfil = url;
            Nome = nome;
            badges.ForEach(b => Badges.Add(new BadgeUsuarioHomeModel(b.Tipo)));
        }
    }
}
