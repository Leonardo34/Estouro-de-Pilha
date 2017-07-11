using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Excecoes
{
    public class ExcecaoUsuarioNaoExistente : Exception
    {
        public ExcecaoUsuarioNaoExistente() : base("O usuário não existe!"){ }
        public ExcecaoUsuarioNaoExistente(string mensagem) : base(mensagem) { }
        public ExcecaoUsuarioNaoExistente(string mensagem, Exception excecaoInterna)
            : base(mensagem, excecaoInterna) { }
    }
}
