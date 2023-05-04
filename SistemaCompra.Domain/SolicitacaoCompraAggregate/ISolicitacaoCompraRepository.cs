using System;
using System.Collections.Generic;

namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface ISolicitacaoCompraRepository
    {
        void Registrar(SolicitacaoCompra solicitacaoCompra);

        SolicitacaoCompra Obter(Guid id);

        IEnumerable<Guid> ObterTodos();
    }
}
