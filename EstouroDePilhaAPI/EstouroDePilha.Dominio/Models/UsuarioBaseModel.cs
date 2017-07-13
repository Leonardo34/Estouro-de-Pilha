using EstouroDePilha.Dominio.Models;
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
        public string Endereco { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Descricao { get; private set; }
        public List<BadgeModel> Badges { get; set; }

        public UsuarioBaseModel(int id, string nome, string email, string urlImagemPerfil, string endereco, DateTime dataCadastro, string descricao)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.UrlImagemPerfil = urlImagemPerfil;
            this.Endereco = endereco;
            this.DataCadastro = dataCadastro;
            this.Descricao = descricao;
            this.Badges = new List<BadgeModel>();
        }

        public UsuarioBaseModel(int id, string nome,  string email, string urlImagemPerfil, string endereco, string descricao)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.UrlImagemPerfil = urlImagemPerfil;
            this.Endereco = endereco;            
            this.Descricao = descricao;
        }
    }
}