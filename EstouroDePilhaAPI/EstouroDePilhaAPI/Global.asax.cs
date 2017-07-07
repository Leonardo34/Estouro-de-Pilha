using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.WebApi;
using EstouroDePilha.Dominio.Repositórios;
using EstouroDePilha.Infraestrutura.Repositórios;
using EstouroDePilha.Infraestrutura;
using System.Data.Entity;

namespace EstouroDePilhaAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register<DbContext, Contexto>(Lifestyle.Scoped);
            container.Register<IPerguntaRepositorio, PerguntaRepositorio>(Lifestyle.Scoped);
            container.Register<IRespostaRepositorio, RespostaRepositorio>(Lifestyle.Scoped);
            container.Register<ITagRepositorio, TagRepositorio>(Lifestyle.Scoped);
            container.Register<IUsuarioRepositorio, UsuarioRepositorio>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
