using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class PerguntaTop5UpvoteModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public PerguntaTop5UpvoteModel(int id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }
    }
}
