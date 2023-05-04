using MediatR;
using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Query
{
    public class ObterSolicitacaoCompraQuery : IRequest<SolicitacaoCompraViewModel>
    {
        public ObterSolicitacaoCompraQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
