using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstouroDePilhaAPI.Models
{
    public class UsuarioBaseModel
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string UrlImagemPerfil { get; private set; }

        public UsuarioBaseModel(int id, string nome, string email, string urlImagemPerfil)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.UrlImagemPerfil = urlImagemPerfil;
        }
    }
}