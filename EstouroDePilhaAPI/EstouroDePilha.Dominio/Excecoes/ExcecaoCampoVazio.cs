using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Excecoes
{
    public class ExcecaoCampoVazio : Exception
    {
        public ExcecaoCampoVazio() { }
        public ExcecaoCampoVazio(string mensagem) : base(mensagem) { }
        public ExcecaoCampoVazio(string mensagem, Exception excecaoInterna)
            : base(mensagem, excecaoInterna) { }
    }
}
