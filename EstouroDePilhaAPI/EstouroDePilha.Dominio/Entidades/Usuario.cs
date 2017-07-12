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

        public UsuarioBaseModel converterUsuarioParaUsuarioModel()
        {
            return new UsuarioBaseModel(this.Id, this.Nome, this.Email, this.UrlFotoPerfil, this.Endereco, this.DataCadastro, this.Descricao);
        }

        public bool AdicionaBadgeGuri()
        {
            int upVotesPergunta = this.Perguntas.Select(p => p.UpVotes.Count()).Count();
            int upVotesResposta = this.Respostas.Select(r => r.UpVotes.Count).Count();
            var badgeGuri = Badges.FirstOrDefault(b => b.Titulo.Contains("Guri"));
            if ((upVotesPergunta + upVotesResposta == 1) && (badgeGuri == null))
            {
                this.Badges.Add(new Badge("Guri", "Usuário recebeu ao menos um upvote"));
                return true;
            }
            return false;
        }

        public bool AdicionaBadgeTramposo()
        {
            
            var ehTramposo = Perguntas.Any(p => p.
            Respostas.Any(r => r.EhRespostaCorreta == true && (r.Usuario.Id == r.Pergunta.Usuario.Id)));
            var badgeTramposo =  Badges.FirstOrDefault(b => b.Titulo.Contains("Guri"));
            if (ehTramposo && badgeTramposo == null)
            {
                this.Badges.Add(new Badge("Tramposo", " Criou a pergunta, respondeu, e marcou a própria resposta como correta."));
                return true;
            }
            return false;
        }
    }
}