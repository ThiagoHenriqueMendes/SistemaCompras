using System;
using System.Collections.Generic;

namespace SistemaCompra.Application.SolicitacaoCompra.Query
{
    public class SolicitacaoCompraViewModel
    {
        public string UsuarioSolicitante  { get; set; }

        public string NomeFornecedor { get; set; }

        public DateTime Data { get; set; }

        public decimal TotalGeral { get; set; }

        public int? CondicaoPagamento { get; set; }

        public string Situacao { get; set; }

        public IEnumerable<ItemSolicitacaoCompraViewModel> Itens { get; set; }
    }
}
