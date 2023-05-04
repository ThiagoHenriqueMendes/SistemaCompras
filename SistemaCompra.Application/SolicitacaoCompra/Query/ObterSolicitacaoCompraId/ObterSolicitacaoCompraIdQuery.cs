using MediatR;
using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterSolicitacaoCompraId
{
    public class ObterSolicitacaoCompraIDQuery : IRequest<SolicitacaoCompraIdViewModel>
    {
        public ObterSolicitacaoCompraIDQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
