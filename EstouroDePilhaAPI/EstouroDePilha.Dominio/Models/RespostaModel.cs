using EstouroDePilhaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class RespostaModel
    {
        public int Id { get; set; }
        public UsuarioBaseModel Usuario { get; set; }
        public String Descricao { get; set; }
        public DateTime DataResposta { get; set; }
        public bool? EhRespostaCorreta { get; set; }
        public int QuantidadeUpVotes { get; set; }
        public int QuantidadeDownVotes { get; set; }
        public List<UsuarioBaseModel> UpVotes { get; set; }
        public List<UsuarioBaseModel> DownVotes { get; set; }
        public List<ComentarioRespostaModel> Comentarios { get; set; }
    }
}
