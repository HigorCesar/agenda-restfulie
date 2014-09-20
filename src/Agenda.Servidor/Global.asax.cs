using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Agenda.Servidor.Model;
using Agenda.Servidor.Persistencia;
using Agenda.Servidor.Servicos;
using Restfulie.Server.Configuration;
using Restfulie.Server.MediaTypes;

namespace Agenda.Servidor
{
    public class Global : HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Get",
                "{controller}/{id}",
                new { action = "Get" },
                new { id = @"\d+", httpMethod = new HttpMethodConstraint("Get") });

            routes.MapRoute(
                "Post",
                "{controller}/",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRoute(
                "Put",
                "{controller}/{id}",
                new { action = "Put" },
                new { httpMethod = new HttpMethodConstraint("PUT") });

            routes.MapRoute(
               "Delete",
               "{controller}/{id}",
               new { action = "Delete" },
               new { httpMethod = new HttpMethodConstraint("Delete") });

            routes.MapRoute(
                "EnviarMensagem",
                "contatos/{id}/mensagens",
                    new { action = "PostMensagem", Controller = "Contatos" },
                new { httpMethod = new HttpMethodConstraint("Post") });

            routes.MapRoute(
                "GetMensagens",
                "contatos/{id}/mensagens",
                    new { action = "GetMensagens", Controller = "Contatos" },
                new { httpMethod = new HttpMethodConstraint("Get") });
            routes.MapRoute(
               "GetMensagem",
               "contatos/{contatoId}/mensagens/{mensagemId}",
                   new { action = "GetMensagem", Controller = "Contatos" },
               new { httpMethod = new HttpMethodConstraint("Get") });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Contatos", action = "GetAll", id = UrlParameter.Optional } // Parameter defaults
            );

            var config = ConfigurationStore.Get();
            config.SetDefaultMediaType<JsonAndHypermedia>();

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);

            var repositorioContatos = new RepositorioContatos();
            var mensageiro = new Mensageiro();
            foreach (var contato in GerarContatosFalsos())
            {
                contato.Id = Convert.ToUInt64(DateTime.Now.ToString("yyyyMMddhhmmssfff")) + (UInt64)new Random().Next(0, 120);
                repositorioContatos.Salvar(contato);
                mensageiro.NotificarContato(contato, "Me ligue: " + contato.Nome);
                Thread.Sleep(1);
            }
        }
        private IEnumerable<Contato> GerarContatosFalsos()
        {
            var contatos = new List<Contato>
            {
                new Contato("Higor", "higor@mail.com"),
                new Contato("Fabricio", "fabricio@mail.com",
                    new[]
                    {
                        new Telefone {Identificador = "Casa", Numero = "981766156"},
                        new Telefone {Identificador = "Trabalho", Numero = "72122334"}
                    }),
                new Contato("Felipe", "felipe@yahoo.com",
                    new[]
                    {
                        new Telefone {Identificador = "Casa", Numero = "2167766251"},
                        new Telefone {Identificador = "Trabalho", Numero = "8767766251"}
                    })
            };
            return contatos;
        }
    }
}