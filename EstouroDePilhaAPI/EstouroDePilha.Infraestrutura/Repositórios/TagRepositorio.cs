using EstouroDePilha.Dominio.Entidades;
using EstouroDePilha.Dominio.Repositórios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Infraestrutura.Repositórios
{
    public class TagRepositorio : ITagRepositorio
    {
        private readonly Contexto contexto;

        public TagRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void Alterar(Tag tag)
        {
            contexto.Entry(tag).State = System.Data.Entity.EntityState.Modified;
        }

        public Dictionary<Tag, int> BuscarTagsUsuarioPorId(int id)
        {
            
            var tags = new Dictionary<Tag, int>();

            var listaPerguntaQueUsuarioRespondeu = contexto.Respostas
                .Where(resposta => resposta.Usuario.Id == id)
                .Select(resposta => new
                {
                    resposta.Pergunta.Id,
                    resposta.Pergunta.Tags,
                    resposta.UpVotes
                }).ToList();                

            foreach(var pergunta in listaPerguntaQueUsuarioRespondeu)
            {
                var upvotes = pergunta.UpVotes.Count; 

                pergunta.Tags.ForEach(tag =>
                {
                    if (tags.ContainsKey(tag))
                        tags[tag] += upvotes;
                    else
                        tags.Add(tag, upvotes);
                });
            }
                 

            return tags;
        }

        public void Criar(Tag tag)
        {
            contexto.Tags.Add(tag);
            contexto.SaveChanges();
        }

        public void Deletar(Tag tag)
        {
            contexto.Tags.Remove(tag);
            contexto.SaveChanges();
        }

        public List<Tag> Listar()
        {
            return contexto.Tags.ToList();
        }

        public Tag ObterPorId(int id)
        {
            return contexto.Tags.FirstOrDefault(t => t.Id == id);
        }
    }
}
