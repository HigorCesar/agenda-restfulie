using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;
using RestfulieClient.resources;
using System.Net;

namespace Agenda.Cliente
{
    public class Contato
    {
        public String Nome { get; set; }
        public String Email { get; set; }
        public Telefone[] Telefones { get; set; }
    }

    public class Telefone
    {
        public String Identificador { get; set; }
        public String Numero { get; set; }
    }

    class Program
    {
        public static void ListarMensagensDeContatos()
        {
            dynamic contatos = Restfulie.At("http://localhost:14617/contatos/").Get();
            foreach (var contato in contatos)
            {
                Console.WriteLine(contato.Nome);
                foreach (var mensagem in contato.Mensagens())
                {
                    Console.WriteLine(mensagem.Texto);
                }
            }
        }

        public static void CriarContato()
        {
            var thiago = new Contato
            {
                Nome = "Thiago",
                Email = "thiago@foo.com",
                Telefones = new[] { new Telefone { Identificador = "UFRJ", Numero = "987711665" } }
            };
            var contatoSerializado = JsonConvert.SerializeObject(thiago);
            Restfulie.At("http://localhost:14617/contatos").Create(contatoSerializado);
        }

        public static void DeletarContato()
        {
            dynamic contatos = Restfulie.At("http://localhost:14617/contatos/").Get();
            contatos[0].Delete();
        }

        static void Main(string[] args)
        {
            dynamic contatos = Restfulie.At("http://localhost:14617/contatos/").Get();
            foreach (var contato in contatos)
                Console.WriteLine(contato.Nome);

            Console.ReadKey();
        }
    }
}
