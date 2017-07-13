using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Excecoes
{
    public class UsuarioJaDeuDownVoteException : Exception
    {
        public UsuarioJaDeuDownVoteException() : base("Você não pode mais dar DownVote nesta pergunta")
        { }
    }
}
