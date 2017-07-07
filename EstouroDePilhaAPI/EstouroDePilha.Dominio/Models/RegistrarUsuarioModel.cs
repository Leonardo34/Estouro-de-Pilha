using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstouroDePilhaAPI.Models
{
    public class RegistrarUsuarioModel : UsuarioBaseModel
    {       
        public string Senha { get; set; }
        public string UrlImagemPerfil { get; set; }

        public RegistrarUsuarioModel(int id, string nome, string email, string senha, string urlImagemPerfil) : base(id, nome, email)
        {
            this.Senha = senha;
            this.UrlImagemPerfil = urlImagemPerfil;
        }
    }
}