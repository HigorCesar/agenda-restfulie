using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agenda.Servidor.Model;
using Agenda.Servidor.Persistencia;

namespace Agenda.Servidor.Servicos
{
    public class Mensageiro
    {
        private readonly RepositorioContatos repositorioContatos;
        public Mensageiro()
        {
            repositorioContatos = new RepositorioContatos();
        }
        public Mensagem NotificarContato(Contato contato, string textoMensagem)
        {
            var mensagem = new Mensagem
            {
                Contato = contato,
                Texto = textoMensagem,
                Identificador = Convert.ToUInt64(DateTime.Now.ToString("yyyyMMddhhmmssfff"))
            };
            contato.EnviarMensagem(mensagem);
            repositorioContatos.Salvar(contato);
            return mensagem;
        }
    }
}