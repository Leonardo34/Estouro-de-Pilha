using EstouroDePilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Usuario : EntidadeBase
    {
        static readonly char[] _caracteresNovaSenha = "abcdefghijklmnopqrstuvzwyz1234567890*-_".ToCharArray();
        static readonly int _numeroCaracteresNovaSenha = 10;

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string UrlFotoPerfil { get; set; }
        public string Senha { get; set; }
        public List<Pergunta> Perguntas { get; set; }
        public List<Resposta> Respostas { get; set; }
        public List<Badge> Badges { get; set; }

        public Usuario(string nome, string endereco, string descricao, string urlFotoPerfil, string email, string senha)
        {
            Nome = nome;
            Endereco = endereco;
            Descricao = descricao;
            UrlFotoPerfil = urlFotoPerfil;
            Email = email;            
            if (!string.IsNullOrWhiteSpace(senha))
                Senha = CriptografarSenha(senha);
            Mensagens = new List<string>();
        }

      
        public Usuario(int id, string nome, string senha, string endereco, string descricao, string urlImagemPerfil)
        {
            Nome = nome;
            if (!string.IsNullOrWhiteSpace(senha))
                Senha = CriptografarSenha(senha);
            Mensagens = new List<string>();
            this.Id = id;
            this.Endereco = endereco;
            this.Descricao = descricao;
            this.UrlFotoPerfil = urlImagemPerfil;
        }

        protected Usuario()
        {
        }

        public string ResetarSenha()
        {
            var senha = string.Empty;
            for (int i = 0; i < _numeroCaracteresNovaSenha; i++)
            {
                senha += new Random().Next(0, _caracteresNovaSenha.Length);
            }

            Senha = CriptografarSenha(senha);

            return senha;
        }

        private string CriptografarSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.Default.GetBytes(Email + senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                sb.Append(hash[i].ToString("x2"));

            return sb.ToString();
        }

        public bool ValidarSenha(string senha)
        {
            return CriptografarSenha(senha) == Senha;
        }

        public override bool EhValida()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Nome))
                Mensagens.Add("Nome é inválido.");

            if (string.IsNullOrWhiteSpace(Email))
                Mensagens.Add("Email é inválido.");

            if (string.IsNullOrEmpty(Endereco))
                Mensagens.Add("Endereco é inválido.");

            if (string.IsNullOrWhiteSpace(UrlFotoPerfil))
                Mensagens.Add("UrlFoto é inválida.");

            if (string.IsNullOrWhiteSpace(Senha))
                Mensagens.Add("Senha é inválida.");

            if (string.IsNullOrEmpty(Descricao))
                Mensagens.Add("Descrição é inválida.");

            return Mensagens.Count == 0;
        }

        public UsuarioBaseModel converterUsuarioParaUsuarioModel ()
        {
            return new UsuarioBaseModel(this.Id, this.Nome, this.Email, this.UrlFotoPerfil, this.Endereco, this.DataCadastro, this.Descricao);
        }


        public bool AdicionaBadgeEntrevero(Badge badge, int idPergunta)
        {
            Pergunta pergunta = Perguntas.FirstOrDefault(p => p.Id == idPergunta);
            var entrevero = pergunta.Respostas.Count > 10;
            if (entrevero)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionaBadgeDeVereda(Badge badge, int idPergunta)
        {
            var resposta = Respostas.FirstOrDefault(r => r.Pergunta.Id == idPergunta);
            var respostas = resposta.Pergunta.Respostas.OrderBy(r => r.DataResposta).ToList();
            var primeiraResposta = Respostas[0].EhRespostaCorreta;
            if (resposta.EhRespostaCorreta == true && primeiraResposta == true)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionaBadgePapudo(Badge badge)
        {
            var badgePapudo = Badges.FirstOrDefault(b => b.Titulo.Contains("Papudo"));
            if (badgePapudo != null)
            {
                return false;
            }
            var papudo = Respostas.Where(r => r.UpVotes.Count() == 0 && r.DownVotes.Count == 0);
            if (papudo.Count() > 10)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }
    }
}