using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Excecoes
{
    public class ExcecaoUsuarioInvalido : Exception
    {
        public ExcecaoUsuarioInvalido() { }
        public ExcecaoUsuarioInvalido(string mensagem) : base(mensagem) { }
        public ExcecaoUsuarioInvalido(string mensagem, Exception excecaoInterna)
            : base(mensagem, excecaoInterna) { }
    }
}
