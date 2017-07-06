using EstouroDePilha.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EstouroDePilhaAPI.Controllers
{
    public abstract class ControllerBase : ApiController
    {
        readonly Contexto contexto;
        public ControllerBase(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public void SalvarAlteracoes()
        {
            this.contexto.SaveChanges();
        }

    }
}
