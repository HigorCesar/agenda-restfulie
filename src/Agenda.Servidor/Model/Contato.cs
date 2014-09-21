using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Servidor.Controllers;
using Restfulie.Server;

namespace Agenda.Servidor.Model
{
    public class Contato : IBehaveAsResource
    {
        public UInt64 Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Telefone[] Telefones { get; set; }

        private readonly List<Mensagem> _mensagens;

        public Contato(string nome, string email, Telefone[] telefones)
            : this(nome, email)
        {

            Telefones = telefones;
        }

        public Contato(string nome, string email)
            : this()
        {
            Nome = nome;
            Email = email;
            Telefones = new Telefone[0];
            _mensagens = new List<Mensagem>();

        }

        public Contato()
        {
            _mensagens = new List<Mensagem>();
        }

        public void SetRelations(Relations relations)
        {
            relations.Named("self").Uses<ContatosController>().Get(Id);
            relations.Named("origin").At(Rotas.Contatos());
            relations.Named("delete").At(Rotas.Contato(Id));
            relations.Named("put").At(Rotas.Contato(Id));
            relations.Named("post").At(Rotas.Contatos());
            relations.Named("mensagens").At(Rotas.Mensagens(Id));
        }

        public void EnviarMensagem(Mensagem mensagem)
        {
            _mensagens.Add(mensagem);
        }

        public IEnumerable<Mensagem> LerMensagens()
        {
            return _mensagens.ToArray();
        }

        public Mensagem? LerMensagem(UInt64 mensagemId)
        {
            var mensagen = _mensagens.FirstOrDefault(x => x.Identificador == mensagemId);
            return mensagen.Identificador != 0 ? mensagen : (Mensagem?)null;
        }
    }
}