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
            var perguntaBuscada = ObterPorId(pergunta.Id);
            contexto.Entry(perguntaBuscada).CurrentValues.SetValues(pergunta);
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
                .ToList();
        }

        public List<Pergunta> ListarPaginado(int skip, int take)
        {
            return contexto.Perguntas
                .Include("Usuario")
                .Include("Tags")
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
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Pergunta> ObterTodasAsPerguntasDaPesquisa(string conteudo, string tags)
        {
            if (conteudo.Contains("undefined"))
            {
                return BuscaPerguntasPorTags(tags);
            }
            else if (tags.Contains("undefined"))
            {
                return BuscaPerguntasPorTituloEDescricao(conteudo);
            }
            else
            {
                return BuscaPerguntasPorTags(tags).Intersect(BuscaPerguntasPorTituloEDescricao(conteudo)).ToList();
            }
        }

        public List<Pergunta> BuscaPerguntasPorTags(string tags)
        {
            return contexto.Perguntas.Include("Tags")
               .Include("Usuario").Where(p => p.Tags.Any(t => t.Descricao.Contains(tags))).ToList();
        }

        public List<Pergunta> BuscaPerguntasPorTituloEDescricao(string conteudoDaBusca)
        {
            var conteudo = conteudoDaBusca.ToLower();
            return contexto.Perguntas.Include("Tags")
              .Include("Usuario").Where(p => p.Titulo.ToLower().Contains(conteudo) || p.Descricao.ToLower().Contains(conteudo)).ToList();
        }

        public List<Pergunta> ObterPerguntasUsuarioPorId(int id)
        {
            return contexto.Perguntas.Where(p => p.Usuario.Id == id).ToList();
        }

        public List<Pergunta> Paginacao(int quantidadePular, string conteudoDaBusca, string tags)
        {
            return ObterTodasAsPerguntasDaPesquisa(conteudoDaBusca, tags)
               .Skip(quantidadePular * 10).Take(10)
               .ToList();
        }

        public int NumeroDeResultadosDaPesquisa(string conteudoDaBusca, string tags)
        {
            return ObterTodasAsPerguntasDaPesquisa(conteudoDaBusca, tags).Count();
        }
    }
}