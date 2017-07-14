using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Models
{
    public class BadgeUsuarioHomeModel
    {
        public string Tipo { get; set; }
        public BadgeUsuarioHomeModel(string tipo) { Tipo = tipo; }
    }
}
