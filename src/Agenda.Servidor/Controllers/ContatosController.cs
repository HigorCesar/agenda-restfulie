using System;
using System.Web.Mvc;
using Agenda.Servidor.Model;
using Restfulie.Server;
using Restfulie.Server.Results;
using Agenda.Servidor.Persistencia;
using Agenda.Servidor.Servicos;
using System.Collections.Generic;

namespace Agenda.Servidor.Controllers
{
    [ActAsRestfulie]
    public class ContatosController : Controller
    {
        private readonly RepositorioContatos _repositorioContatos;
        private readonly Mensageiro _mensageiro;

        public ContatosController()
        {
            _repositorioContatos = new RepositorioContatos();
            _mensageiro = new Mensageiro();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult GetAll()
        {
            return new OK(_repositorioContatos.Listar());
        }

        public virtual ActionResult Get(UInt64 id)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(id);
            if (contatoEncontrado == null)
                return new NotFound();

            return new OK(contatoEncontrado);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Post(Contato contato)
        {
            contato.Id = Convert.ToUInt64(DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            _repositorioContatos.Salvar(contato);
            return new Created(contato, Rotas.Contato(contato.Id));
        }

        [AcceptVerbs(HttpVerbs.Put)]
        public virtual ActionResult Put(UInt64 id, Contato contato)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(id);
            if (contatoEncontrado == null)
                return Post(contato);

            contato.Id = contatoEncontrado.Id;
            _repositorioContatos.Salvar(contato);
            return new OK(contato);
        }

        [AcceptVerbs(HttpVerbs.Delete)]
        public virtual ActionResult Delete(UInt64 id)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(id);
            if (contatoEncontrado == null)
                return new NotFound();

            _repositorioContatos.Remover(contatoEncontrado);
            return new OK();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult PostMensagem(UInt64 id, string mensagem)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(id);
            if (contatoEncontrado == null)
                return new NotFound();

            var mensagemEnviada = _mensageiro.NotificarContato(contatoEncontrado, mensagem);
            return new Created(mensagemEnviada, Rotas.Mensagem(contatoEncontrado.Id, mensagemEnviada.Identificador));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult GetMensagens(UInt64 id)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(id);
            if (contatoEncontrado == null)
                return new NotFound();

            return new OK(contatoEncontrado.LerMensagens());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual ActionResult GetMensagem(UInt64 contatoId, UInt64 mensagemId)
        {
            var contatoEncontrado = _repositorioContatos.Buscar(contatoId);
            if (contatoEncontrado == null)
                return new NotFound();
            var mensagem = contatoEncontrado.LerMensagem(mensagemId);
            if (mensagem == null)
                return new NotFound();

            return new OK(mensagem);
        }
    }
}