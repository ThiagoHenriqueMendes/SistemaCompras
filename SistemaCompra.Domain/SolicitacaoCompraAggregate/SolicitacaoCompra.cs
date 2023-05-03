using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.ProdutoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra : Entity
    {
        public UsuarioSolicitante UsuarioSolicitante { get; private set; }
        public NomeFornecedor NomeFornecedor { get; private set; }
        public IList<Item> Itens { get; private set; }
        public DateTime Data { get; private set; }
        public Money TotalGeral { get; private set; }
        public Situacao Situacao { get; private set; }
        public CondicaoPagamento CondicaoPagamento { get; private set; }

        private SolicitacaoCompra() { }

        public SolicitacaoCompra(string usuarioSolicitante, string nomeFornecedor)
        {
            Id = Guid.NewGuid();
            UsuarioSolicitante = new UsuarioSolicitante(usuarioSolicitante);
            NomeFornecedor = new NomeFornecedor(nomeFornecedor);
            Data = DateTime.Now;
            Situacao = Situacao.Solicitado;
        }

        public void AdicionarItem(Produto produto, int qtde)
        {
            Itens.Add(new Item(produto, qtde));
            RegistraTotalGeral();
        }

        public void RegistrarCompra(IEnumerable<Item> itens)
        {
            if (itens is null && itens.Any() is false)
                return;

            Itens = itens.ToList();

            RegistraTotalGeral();
        }

        private void RegistraTotalGeral()
        {
            TotalGeral = new Money(Itens.Sum(x => x.Subtotal.Value));
            DefiniCondicaoPagamento();
        }


        private void DefiniCondicaoPagamento()
        {
            if (TotalGeral is null)
                return;

            CondicaoPagamento = new CondicaoPagamento(PrazoDiasPagamento(TotalGeral.Value));
        }


        private static int PrazoDiasPagamento(decimal valorTotal)
        {
            if (valorTotal > 50000) return 30; //30 dias prazo

            return default;
        }



    }
}
