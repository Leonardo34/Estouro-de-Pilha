using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class ComentarioPergunta
    {
        public int Id { get; private set; }
        public virtual Pergunta Pergunta { get; private set; }
        public virtual Usuario Usuario { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataComentario { get; private set; }

        public ComentarioPergunta()
        {
        }

        public ComentarioPergunta(Pergunta pergunta, Usuario usuario, string descricao)
        {
            Pergunta = pergunta;
            Usuario = usuario;
            Descricao = descricao;
            DataComentario = DateTime.Now;
        }
    }
}
