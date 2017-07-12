using EstouroDePilha.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstouroDePilha.Dominio.Servicos
{
    public interface IBadgeService
    {
        void ChecarBadges(Usuario usuario);
    }
}
