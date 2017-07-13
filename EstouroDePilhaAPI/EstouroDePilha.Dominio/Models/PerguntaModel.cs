using EstouroDePilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class PerguntaModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public UsuarioBaseModel Usuario { get; set; }
        public List<int> TagsIds { get; set; }
        public List<TagModel> Tags { get; set; }
        public DateTime DataPergunta { get; set; }
        public int QuantidadeUpVotes { get; set; }
        public int QuantidadeDownVotes { get; set; }
        public List<UsuarioBaseModel> UpVotes { get; set; }
        public List<UsuarioBaseModel> DownVotes { get; set; }
        public List<ComentarioRespostaModel> Comentarios { get; set; }
    }
}