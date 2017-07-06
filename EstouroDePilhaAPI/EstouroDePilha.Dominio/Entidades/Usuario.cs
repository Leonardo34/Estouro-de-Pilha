using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Entidades
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public string UrlFotoPerfil { get; set; }
        public string Senha { get; set; }
    }
}
