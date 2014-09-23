using System;
namespace Agenda.Servidor
{
    public class Rotas
    {
        public static string Contato(UInt64 id)
        {
            return Contatos() + id;
        }

        public static string Contatos()
        {
            return Configuracoes.UrlBase + "contatos/";
        }
        public static string Mensagens(UInt64 contatoId)
        {
            return Contato(contatoId) + "/mensagens/";
        }
        public static string Mensagem(UInt64 contatoId, UInt64 mensagemId)
        {
            return Mensagens(contatoId) + mensagemId;
        }
    }
}