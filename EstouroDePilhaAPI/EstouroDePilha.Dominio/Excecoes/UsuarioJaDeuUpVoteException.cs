using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Excecoes
{
    public class UsuarioJaDeuUpVoteException : Exception
    {
        public UsuarioJaDeuUpVoteException() : base("Você não pode mais dar UpVote nesta pergunta")
        {  }
    }
}
