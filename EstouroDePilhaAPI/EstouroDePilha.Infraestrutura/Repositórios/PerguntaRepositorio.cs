using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using EstouroDePilha.Dominio.Models;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class PerguntaRepositorio : IPerguntaRepositorio
    {
        private readonly Contexto contexto;

        public PerguntaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Pergunta pergunta)
        {
            contexto.Entry(pergunta).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Criar(Pergunta pergunta)
        {
            contexto.Perguntas.Add(pergunta);
            contexto.SaveChanges();
        }

        public void Deletar(Pergunta pergunta)
        {
            contexto.Perguntas.Remove(pergunta);
            contexto.SaveChanges();
        }

        public List<Pergunta> Listar()
        {
            return contexto.Perguntas
                .Include("Usuario")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Include("UpVotes")
                .Include("UpVotes.Usuario")
                .ToList();
        }

        public List<Pergunta> ListarPaginado(int skip, int take)
        {
            return contexto.Perguntas
                .Include("Usuario")
                .Include("Tags")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Include("UpVotes")
                .Include("UpVotes.Usuario")
                .OrderByDescending(p => p.DataPergunta)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public Pergunta ObterPorId(int id)
        {
            return contexto.Perguntas
                .Include("Usuario")
                .Include("Respostas")
                .Include("Tags")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Include("UpVotes")
                .Include("UpVotes.Usuario")
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Pergunta> ObterTodasAsPerguntasDaPesquisa(string conteudo, string tags)
        {
            List<Pergunta> perguntasDaPesquisa;
            if (conteudo.Contains("undefined"))
            {
                perguntasDaPesquisa = BuscaPerguntasPorTags(tags);
                return RetornarPerguntasOrdenadasPorMaiorNumeroDeUpVotes(perguntasDaPesquisa);
            }
            else if (tags == null)
            {
                perguntasDaPesquisa = BuscaPerguntasPorTituloEDescricao(conteudo);
                return RetornarPerguntasOrdenadasPorMaiorNumeroDeUpVotes(perguntasDaPesquisa);
            }
            else
            {
                perguntasDaPesquisa = BuscaPerguntasPorTags(tags).Intersect(BuscaPerguntasPorTituloEDescricao(conteudo)).ToList();
                return RetornarPerguntasOrdenadasPorMaiorNumeroDeUpVotes(perguntasDaPesquisa);
            }
        }

        public List<Pergunta> BuscaPerguntasPorTags(string tags)
        {
            return contexto.Perguntas.Include("Tags")
               .Include("Usuario")
               .Include("UpVotes")
               .Include("DownVotes")
               .Include("DownVotes.Usuario")
               .Include("UpVotes.Usuario")
               .Where(p => p.Tags.Any(t => t.Descricao.Equals(tags))).ToList();         
        }

        public List<Pergunta> RetornarPerguntasOrdenadasPorMaiorNumeroDeUpVotes(List <Pergunta> perguntas)
        {
            return perguntas.OrderByDescending(p => p.UpVotes.Count() - p.DownVotes.Count()).ToList();
        }

        public List<Pergunta> BuscaPerguntasPorTituloEDescricao(string conteudoDaBusca)
        {
            var conteudo = conteudoDaBusca.ToLower();
            return contexto.Perguntas
                .Include("Tags")
                .Include("Usuario")
                .Include("UpVotes")
                .Include("DownVotes")
                .Include("DownVotes.Usuario")
                .Include("UpVotes.Usuario")
                .Where(p => p.Titulo.ToLower().Contains(conteudo) 
                        || p.Descricao.ToLower().Contains(conteudo))
                .ToList();
        }

        public List<PerguntaPerfilModel> ObterTop5PerguntasUsuarioPorId(int id)
        {
            List<PerguntaPerfilModel> perguntasPerfil = new List<PerguntaPerfilModel>();
            var perguntasUsuario = contexto.Perguntas
                .Where(p => p.Usuario.Id == id)
                .OrderByDescending(p => p.DataPergunta)
                .Take(5)
                .Select(p => new { p.Titulo, p.Id })
                .ToList();

            perguntasUsuario.ForEach(p => perguntasPerfil.Add(new PerguntaPerfilModel(p.Id, p.Titulo)));
            return perguntasPerfil;          
        }

        public List<Pergunta> ObterResultadosDaBuscaPaginados(int quantidadePular, string conteudoDaBusca, string tags)
        {
            return ObterTodasAsPerguntasDaPesquisa(conteudoDaBusca, tags)
               .Skip(quantidadePular * 10).Take(10)
               .ToList();
        }

        public List<Pergunta> ObterTop5PerguntasMaiorNumUpvotes()
        {
            return contexto.Perguntas
                .OrderByDescending(p => p.UpVotes.Count)
                .Take(5)
                .ToList();
        }

        public int NumeroDeResultadosDaPesquisa(string conteudoDaBusca, string tags)
        {
            return ObterTodasAsPerguntasDaPesquisa(conteudoDaBusca, tags).Count();
        }

        public int TotalPerguntasCadastradas()
        {
            return contexto.Perguntas.Count();
        }

        public void AdicionarUpvote(UpVotePergunta upvote)
        {
            contexto.UpVotesPerguntas.Add(upvote);
            contexto.SaveChanges();
        }

        public void AdicionarDownvote(DownVotePergunta downvote)
        {
            contexto.DownVotesPerguntas.Add(downvote);
            contexto.SaveChanges();
        }
    }
}