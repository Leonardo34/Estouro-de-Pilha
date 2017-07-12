using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

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
            else if (tags.Contains("undefined"))
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
               .Where(p => p.Tags.Any(t => t.Descricao.Contains(tags))).ToList();         
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
                .Where(p => p.Titulo.ToLower().Contains(conteudo) 
                        || p.Descricao.ToLower().Contains(conteudo))
                .ToList();
        }

        public List<Pergunta> ObterPerguntasUsuarioPorId(int id)
        {
            return contexto.Perguntas.Where(p => p.Usuario.Id == id).ToList();
        }

        public List<Pergunta> ObterResultadosDaBuscaPaginados(int quantidadePular, string conteudoDaBusca, string tags)
        {
            return ObterTodasAsPerguntasDaPesquisa(conteudoDaBusca, tags)
               .Skip(quantidadePular * 10).Take(10)
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