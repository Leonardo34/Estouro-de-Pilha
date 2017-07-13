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
                usuarioModel.Badges.Add(badgeModel);
            }
            return usuarioModel;
        }

        public bool AdicionarBadgePeleador(Badge badge, int idPergunta)
        {
            var respostaCorreta = Respostas
                .FirstOrDefault(r => r.Pergunta.Id == idPergunta).Pergunta
                .Respostas.FirstOrDefault(r => r.EhRespostaCorreta == true);
            var respostas = Respostas.Where(r => r.Pergunta.Id == idPergunta);
            var respostasPeleadoras = respostas
                .Where(r => r.UpVotes.Count() - respostaCorreta.UpVotes.Count() > 10 && r.Usuario.Id == Id);
            var numeroDeRespostasPeleadoras = respostasPeleadoras.Count();
            var numeroDeBadgesPeleadoras = this.Badges.Where(b => b.Titulo.Contains("Peleador")).Count();
            if (numeroDeRespostasPeleadoras > numeroDeBadgesPeleadoras)
            {
                while (numeroDeRespostasPeleadoras > numeroDeBadgesPeleadoras)
                {
                    this.Badges.Add(badge);
                    numeroDeBadgesPeleadoras++;
                }
                return true;
            }
            return false;
        }

        public bool AdicionaBadgeEntrevero(Badge badge, int idPergunta)
        {
            Pergunta pergunta = Perguntas.FirstOrDefault(p => p.Id == idPergunta);
            var entrevero = pergunta.Respostas.Count() > 10;
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

        public bool AdicionaBadgeGuri(Badge badge)
        {
            int upVotesPergunta = this.Perguntas.Select(p => p.UpVotes.Count()).Sum();
            int upVotesResposta = this.Respostas.Select(r => r.UpVotes.Count).Sum();
            var badgeGuri = Badges.FirstOrDefault(b => b.Titulo.Contains("Guri"));
            if ((upVotesPergunta + upVotesResposta == 1) && (badgeGuri == null))
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionaBadgePapudo(Badge badge)
        {
            var papudo = Respostas.Where(r => r.UpVotes.Count() == 0 && r.DownVotes.Count == 0);
            var badgePapudo = Badges.FirstOrDefault(b => b.Titulo.Contains("Papudo"));
            if (papudo.Count() > 10 && badgePapudo == null)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool AdicionaBadgeTramposo(Badge badge)
        {
            var ehTramposo = Perguntas.Any(p => p.
                Respostas.Any(r => r.EhRespostaCorreta == true && (r.Usuario.Id == r.Pergunta.Usuario.Id)));
            var badgeTramposo = Badges.FirstOrDefault(b => b.Titulo.Contains("Tramposo"));
            if (ehTramposo && badgeTramposo == null)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }


        public bool AdicionaBadgeAmargo(Badge badge, int numeroDownVotes)
        {
            if (numeroDownVotes != 6) return false;

            this.Badges.Add(badge);
            return true;
        }

        public bool AdicionarBadgeFaceiro(Badge badge, List<UpVotePergunta> upVotePergunta, List<UpVoteResposta> upVoteResposta)
        {
            if (this.Badges.FirstOrDefault(b => b.Titulo.Contains("Faceiro")) != null)
            {
                return false;
            }
            var upVoteRespostaDatas = upVoteResposta.Select(up => up.Data).ToList();
            var upVotePerguntaDatas = upVotePergunta.Select(up => up.Data).ToList();
            var datasDeUpVotes = upVotePerguntaDatas.Concat(upVoteRespostaDatas).OrderBy(x => x.TimeOfDay).ToList();

            double diferencaDeTempo = 0;
            foreach (DateTime data in datasDeUpVotes)
            {
                foreach (DateTime data1 in datasDeUpVotes)
                {
                    var contador = 1;
                    diferencaDeTempo += (data - data1).TotalSeconds;
                    contador++;
                    if (diferencaDeTempo > 60 && contador == 5)
                    {
                        diferencaDeTempo = 0;
                        break;
                    }
                    if (diferencaDeTempo < 60 && contador == 5)
                    {
                        this.Badges.Add(badge);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AdicionarBadgeEsgualepado(Badge badge)
        {
            if (this.Badges.FirstOrDefault(b => b.Titulo.Contains("Esgualepado")) != null)
            {
                return false;
            }
            List<DateTime> datasDeUpVotes = new List<DateTime>();
            this.Respostas.ForEach(r => r.UpVotes.ForEach(up => datasDeUpVotes.Add(up.Data)));
            this.Perguntas.ForEach(p => p.UpVotes.ForEach(up => datasDeUpVotes.Add(up.Data)));
            double diferencaDeTempo = 0;
            var UpVotesOrdenadosPorData = datasDeUpVotes.OrderBy(x => x.TimeOfDay).ToList();
            foreach (DateTime data in datasDeUpVotes)
            {
                foreach (DateTime data1 in UpVotesOrdenadosPorData)
                {
                    var contador = 1;
                    diferencaDeTempo += (data - data1).TotalSeconds;
                    contador++;
                    if (diferencaDeTempo > 30 && contador == 3)
                    {
                        diferencaDeTempo = 0;
                        break;
                    }
                    if (diferencaDeTempo < 30 && contador == 3)
                    {
                        this.Badges.Add(badge);
                        return true;
                    }

                }
            }
            return false;
        }


        public bool AdicionarBadgeBaitaPergunta(Badge badge, int idPergunta)
        {
            var upVotesPergunta = Perguntas.FirstOrDefault(p => p.Id == idPergunta).UpVotes.Count();
            if (upVotesPergunta == 16)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool adicionarbadgeEmbretado(Badge badge)
        {
            if (this.Badges.FirstOrDefault(b => b.Titulo.Contains("Embretado")) != null)
            {
                return false;
            }
            var temPerguntaIgnorada = this.Perguntas.FirstOrDefault(p => (DateTime.Now - p.DataPergunta).TotalDays == 7
            && p.Respostas.Count() == 0);
            if (temPerguntaIgnorada != null)
            {
                this.Badges.Add(badge);
            }
            return false;
        }

        public bool AdicionarBadgeGauderio(Badge badge)
        {
            var gauderio = Badges.FirstOrDefault(b => b.Titulo.Contains("Gaudério"));
            if (gauderio != null)
            {
                return false;
            }
            var upVotesPerguntas = Perguntas.Sum(p => p.UpVotes.Count);
            var upVotesRespostas = Respostas.Sum(r => r.UpVotes.Count);
            if ((upVotesPerguntas + upVotesRespostas) > 20)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }

        public bool adicionarbadgeGuriDeApartamento(Badge badge, int numeroDeVotos)
        {
            if (this.Badges.FirstOrDefault(b => b.Titulo.Contains("Guri de apartamento")) != null)
            {
                return false;
            }
            var temContaAMaisDeUmAno = (DateTime.Today - this.DataCadastro).TotalDays == (DateTime.Now - DateTime.Now.AddYears(-1)).TotalDays;
            var nuncaPerguntou = this.Perguntas.Count() == 0;
            if (temContaAMaisDeUmAno && nuncaPerguntou && (numeroDeVotos == 0))
            {
                this.Badges.Add(badge);
            }
            return false;
        }

        public bool AdicionarBadgeGaloVeio(Badge badge)
        {
            var ehGuriDeApartamento = this.Badges.FirstOrDefault(b => b.Titulo.Contains("Guri de apartamento")) != null;
            var ehGauderio = Badges.FirstOrDefault(b => b.Titulo.Contains("Gauderio")) != null;
            if (ehGuriDeApartamento || !ehGauderio)
            {
                return false;
            }
            var passouTresAnos = (DateTime.Now - this.DataCadastro).TotalDays == (DateTime.Now - DateTime.Now.AddYears(-3)).TotalDays;
            var respondeuMaisDe30Vezes = this.Respostas.Count() > 30;
            if (passouTresAnos && respondeuMaisDe30Vezes)
            {
                this.Badges.Add(badge);
                return true;
            }
            return false;
        }
    }
}