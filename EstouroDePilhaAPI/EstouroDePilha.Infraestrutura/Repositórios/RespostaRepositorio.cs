using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class RespostaRepositorio : IRespostaRepositorio
    {
        private readonly Contexto contexto;

        public RespostaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Resposta resposta)
        {
            contexto.Entry(resposta).State = System.Data.Entity.EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Criar(Resposta resposta)
        {
            contexto.Respostas.Add(resposta);
            contexto.SaveChanges();
        }

        public void Deletar(Resposta resposta)
        {
            contexto.Respostas.Remove(resposta);
            contexto.SaveChanges();
        }

        public List<Resposta> Listar()
        {
            return contexto.Respostas.ToList();
        }

        public Resposta ObterPorId(int id)
        {
            return contexto.Respostas.FirstOrDefault(r => r.Id == id);
        }

        public List<Resposta> ObterRespostasPorUsuarioId(int id)
        {
            return contexto.Respostas.Where(p => p.Usuario.Id == id).ToList();
        }


        public List<Resposta> ObterRespostasPeloIdPergunta(int id)
        {
            return contexto.Respostas
                .Include("Usuario")
                .Where(r => r.Pergunta.Id == id)
                .ToList();
        }
    }
}
