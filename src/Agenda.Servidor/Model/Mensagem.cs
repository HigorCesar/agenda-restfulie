using System;
using Restfulie.Server;

namespace Agenda.Servidor.Model
{
    public struct Mensagem : IBehaveAsResource
    {
        public UInt64 Identificador { get; set; }
        public string Texto { get; set; }
        public Contato Contato { get; set; }
        public void SetRelations(Relations relations)
        {
            relations.Named("self").At(Rotas.Mensagem(Contato.Id, Identificador));
            relations.Named("origin").At(Rotas.Mensagens(Contato.Id));

        }
    }
}