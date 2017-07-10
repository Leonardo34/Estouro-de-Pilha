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
        public HttpResponseMessage ResponderOK(object result = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { result });
        }

        public HttpResponseMessage ResponderOk(object dados = null, object outrosDados = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new {dados, outrosDados});
        }

        public HttpResponseMessage ResponderLogin(object dados = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { dados });
        }

        public HttpResponseMessage ResponderErro(params string[] errors)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors });
        }

        public HttpResponseMessage ResponderErro(IEnumerable<string> errors)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors });
        }
    }
}
