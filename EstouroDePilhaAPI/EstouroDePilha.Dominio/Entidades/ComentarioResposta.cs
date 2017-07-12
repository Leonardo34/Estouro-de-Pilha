using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class ComentarioResposta
    {
        public int Id { get; private set; }
        public Resposta Resposta { get; private set; }
        public Usuario Usuario { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataComentario { get; private set; }

        protected ComentarioResposta()
        {
        }

        public ComentarioResposta(Resposta resposta, Usuario usuario, string descricao)
        {
            Resposta = resposta;
            Usuario = usuario;
            Descricao = descricao;
            DataComentario = DateTime.Now;
        }
    }
}
