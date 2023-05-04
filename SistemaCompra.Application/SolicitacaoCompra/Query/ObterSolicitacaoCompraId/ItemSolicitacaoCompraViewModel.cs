using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterSolicitacaoCompraId
{
    public class ItemSolicitacaoCompraViewModel
    {
        public Guid Id { get; set; }

        public int Qtde { get; set; }

        public decimal Subtotal { get; set; }

        public Guid ProdutoId { get; set; }
    }
}
