using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterTodosSolicitacaoCompra
{
    public class SolicitacaoCompraQueryHandler : IRequestHandler<ObterSolicitacaoCompraQuery, IEnumerable<Guid>>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public SolicitacaoCompraQueryHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }
        public Task<IEnumerable<Guid>> Handle(ObterSolicitacaoCompraQuery request, CancellationToken cancellationToken)
        {
            var listaSolicitacaoCompra = _solicitacaoCompraRepository.ObterTodos();

            return Task.FromResult(listaSolicitacaoCompra);
        }
    }
}
