using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Agenda.Servidor.Model;

namespace Agenda.Servidor.Persistencia
{
    public class RepositorioContatos
    {
        private static readonly IList<Contato> Contatos = new List<Contato>();

        public void Salvar(Contato contato)
        {
            var contatoEncontrado = Contatos.FirstOrDefault(x => x.Id == contato.Id);
            if (contatoEncontrado != null)
                Contatos.Remove(contatoEncontrado);

            Contatos.Add(contato);
        }

        public void Remover(Contato contato)
        {
            Contatos.Remove(contato);
        }

        public IList<Contato> Listar()
        {
            return Contatos;
        }

        public Contato Buscar(UInt64 id)
        {
            return Contatos.FirstOrDefault(x => x.Id == id);
        }
    }
}