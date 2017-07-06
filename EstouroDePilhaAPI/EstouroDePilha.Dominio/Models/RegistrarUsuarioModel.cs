using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstouroDePilhaAPI.Models
{
    public class RegistrarUsuarioModel : UsuarioBaseModel
    {       
        public string Senha { get; set; }

        public RegistrarUsuarioModel(int id, string email, string senha) : base (id,email, senha)
        {
            this.Senha = senha;
        }
    }
}