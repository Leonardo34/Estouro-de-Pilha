using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //.Include("Usuario")
                .ToList();
        }

        public Pergunta ObterPorId(int id)
        {
            return contexto.Perguntas
                .Include("Usuario")
                .Include("Tags")
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Pergunta> ObterPerguntasPeloTitulo(string titulo)
        {
            return contexto.Perguntas
                .Include("Tags")
                .Include("Usuario")
                .Where(p => p.Titulo.Contains(titulo)).OrderByDescending(p => p.DataPergunta)
                .ToList();
        }

        public List<Pergunta> Paginacao(string titulo, int quantidadePular)
        {
            return ObterPerguntasPeloTitulo(titulo)
               .Skip(quantidadePular*10).Take(10)
               .ToList();
        }
    }
}