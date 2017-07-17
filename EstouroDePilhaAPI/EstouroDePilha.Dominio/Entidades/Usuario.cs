using EstouroDePilha.Dominio.Models;
using EstouroDePilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
        public virtual List<Pergunta> Perguntas { get; set; }
        public virtual List<Resposta> Respostas { get; set; }
        public virtual List<Badge> Badges { get; set; }
        public object Datetime { get; private set; }

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
            Badges = new List<Badge>();
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
            var usuarioModel = new UsuarioBaseModel(this.Id, this.Nome, this.Email, this.UrlFotoPerfil, this.Endereco, this.DataCadastro, this.Descricao);
            foreach (var badge in Badges)
            {
                var badgeModel = new BadgeModel();
                badgeModel.Id = badge.Id;
                badgeModel.Titulo = badge.Titulo;
                badgeModel.Descricao = badge.Descricao;
                badgeModel.Tipo = badge.Tipo;
                usuarioModel.Badges.Add(badgeModel);
            }
            return usuarioModel;
        }

        public bool AdicionarBadgePeleador(Badge badge, int idPergunta)
        {
            var respostaCorreta = Respostas?
                .FirstOrDefault(r => r?.Pergunta.Id == idPergunta).Pergunta?
                .Respostas.FirstOrDefault(r => r.EhRespostaCorreta == true);
            var respostas = Respostas?.Where(r => r.Pergunta?.Id == idPergunta).ToList();
            var respostasPeleadoras = respostas
                ?.Where(r => r?.UpVotes.Count - respostaCorreta?.UpVotes.Count > 10 && r.Usuario.Id == Id);
            var numeroDeRespostasPeleadoras = respostasPeleadoras?.Count();
            var numeroDeBadgesPeleadoras = this.Badges?.Where(b => b.Titulo.Contains("Peleador")).Count();
            if (numeroDeRespostasPeleadoras > numeroDeBadgesPeleadoras)
            {
                while (numeroDeRespostasPeleadoras > numeroDeBadgesPeleadoras)
                {
                    this.Badges?.Add(badge);
                    numeroDeBadgesPeleadoras++;
                }
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeEntrevero(Badge badge, int idPergunta)
        {
            Pergunta pergunta = Perguntas?.FirstOrDefault(p => p.Id == idPergunta);
            var entrevero = pergunta?.Respostas.Count == 11;
            if (entrevero)
            {
                this.Badges?.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeDeVereda(Badge badge, int idPergunta)
        {
            var resposta = this.Respostas?.FirstOrDefault(r => r.Pergunta.Id == idPergunta);
            var respostas = resposta?.Pergunta?.Respostas?.OrderBy(r => r.DataResposta).ToList();
            if (respostas == null)
            {
                return false;
            }
            var primeiraResposta = respostas[0].EhRespostaCorreta;
            if (resposta.EhRespostaCorreta == true && primeiraResposta == true)
            {
                this.Badges?.Add(badge);
                return true;

            }
            return false;
        }

        public bool AdicionarBadgeGuri(Badge badge)
        {
            int upVotesResposta = 0;
            int upVotesPergunta = 0;
            if (this.Perguntas != null)
            {
                upVotesPergunta = this.Perguntas.Select(p => p.UpVotes.Count()).Sum();
            }
            if (this.Respostas != null)
            {
                upVotesResposta = this.Respostas.Select(r => r.UpVotes.Count).Sum();
            }
            var badgeGuri = Badges?.FirstOrDefault(b => b.Titulo.Contains("Guri"));
            if ((upVotesPergunta + upVotesResposta == 1) && (badgeGuri == null))
            {
                this.Badges?.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgePapudo(Badge badge)
        {
            if (this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Papudo")) != null)
            {
                return false;
            }
            var papudo = Respostas?.Where(r => r?.UpVotes.Count == 0 && r?.DownVotes.Count == 0).ToList();
            if (papudo?.Count() > 10)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeTramposo(Badge badge)
        {
            if (this.Perguntas == null)
            {
                return false;
            }
            var ehTramposo = this.Perguntas.Any(p => p.
                Respostas.Any(r => r.EhRespostaCorreta == true && (r.Usuario.Id == r.Pergunta.Usuario.Id)));
            var badgeTramposo = Badges.FirstOrDefault(b => b.Titulo.Contains("Tramposo"));
            if (ehTramposo && badgeTramposo == null)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }


        public bool AdicionarBadgeAmargo(Badge badge, int numeroDownVotes)
        {
            if (numeroDownVotes != 6) return false;

            this.Badges.Add(badge);
            return true;
        }

        public bool AdicionarBadgeFaceiro(Badge badge, List<UpVotePergunta> upVotePergunta, List<UpVoteResposta> upVoteResposta)
        {
            if (this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Faceiro")) != null)
            {
                return false;
            }
            var upVoteRespostaDatas = upVoteResposta?.Select(up => up.Data).ToList();
            var upVotePerguntaDatas = upVotePergunta?.Select(up => up.Data).ToList();
            var datasDeUpVotes = upVotePerguntaDatas?.Concat(upVoteRespostaDatas)?.OrderByDescending(x => x.TimeOfDay).ToList();
            if (datasDeUpVotes.Count < 5)
            {
                return false;
            }
            var dataDosUltimos5UpVotes = datasDeUpVotes.Skip(0).Take(5).ToList();
            var diferencaEntreOPrimeiroEOQuintoUpVote = (dataDosUltimos5UpVotes[0] - dataDosUltimos5UpVotes[4]).TotalSeconds;
            if (diferencaEntreOPrimeiroEOQuintoUpVote < 60)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeEsgualepado(Badge badge)
        {
            if (this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Esgualepado")) != null)
            {
                return false;
            }
            List<DateTime> datasDeUpVotes = new List<DateTime>();
            this.Respostas?.ForEach(r => r?.UpVotes.ForEach(up => datasDeUpVotes.Add(up.Data)));
            this.Perguntas?.ForEach(p => p?.UpVotes.ForEach(up => datasDeUpVotes.Add(up.Data)));
            var upVotesOrdenadosPorData = datasDeUpVotes.OrderByDescending(x => x.TimeOfDay).ToList();
            if (upVotesOrdenadosPorData.Count < 3)
            {
                return false;
            }
            var dataDosUltimos3UpVotes = upVotesOrdenadosPorData.Skip(0).Take(3).ToList();
            var diferencaEntreOPrimeiroEOTerceiroUpVote = (dataDosUltimos3UpVotes[0] - dataDosUltimos3UpVotes[2]).TotalSeconds;
            if (diferencaEntreOPrimeiroEOTerceiroUpVote < 30)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeBaitaPergunta(Badge badge, int idPergunta)
        {
            var upVotesPergunta = this.Perguntas?.FirstOrDefault(p => p.Id == idPergunta)?.UpVotes.Count;
            if (upVotesPergunta == 16)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeEmbretado(Badge badge)
        {
            if (this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Embretado")) != null)
            {
                return false;
            }
            var temPerguntaIgnorada = this.Perguntas?.FirstOrDefault(p => (DateTime.Now - p.DataPergunta).TotalDays > 7
                    && p.Respostas?.Count == 0);
            if (temPerguntaIgnorada != null)
            {
                this.Badges?.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarBadgeGauderio(Badge badge)
        {
            var gauderio = this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Gaudério"));
            if (gauderio != null)
            {
                return false;
            }
            var upVotesPerguntas = this.Perguntas?.Sum(p => p?.UpVotes.Count);
            var upVotesRespostas = this.Respostas?.Sum(r => r?.UpVotes.Count);
            if ((upVotesPerguntas + upVotesRespostas) > 20)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionarbadgeGuriDeApartamento(Badge badge, int numeroDeVotos)
        {
            if (this.Badges.FirstOrDefault(b => b.Titulo.Contains("Guri de apartamento")) != null)
            {
                return false;
            }
            var temContaAMaisDeUmAno = (DateTime.Now - this.DataCadastro).TotalDays >= 365;
            var nuncaPerguntou = this?.Perguntas.Count() == 0;
            if (temContaAMaisDeUmAno && nuncaPerguntou && (numeroDeVotos == 0))
            {
                this.Badges.Add(badge);
            }
            return false;
        }

        public bool AdicionarBadgeGaloVeio(Badge badge)
        {
            var ehGuriDeApartamento = this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Guri de apartamento")) != null;
            var ehGauderio = this.Badges?.FirstOrDefault(b => b.Titulo.Contains("Gaudério")) != null;
            if (ehGuriDeApartamento || !ehGauderio)
            {
                return false;
            }         
            var passouTresAnos = (DateTime.Now - this.DataCadastro).TotalDays > (DateTime.Now -  DateTime.Now.AddYears(-3)).TotalDays;
            var respondeuMaisDe30Vezes = this.Respostas?.Count > 30;
            if (passouTresAnos && respondeuMaisDe30Vezes)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }
    }
}