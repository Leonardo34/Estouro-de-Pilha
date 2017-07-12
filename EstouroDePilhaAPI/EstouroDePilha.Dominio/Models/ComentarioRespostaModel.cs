using EstouroDePilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class ComentarioRespostaModel
    {
        public int Id { get; set; }
        public UsuarioBaseModel Usuario { get; set; }
        public string Descricao { get; set; }
        public DateTime DataComentario { get; set; }
    }
}
