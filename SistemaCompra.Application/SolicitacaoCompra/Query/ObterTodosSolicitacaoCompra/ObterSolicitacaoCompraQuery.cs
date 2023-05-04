using MediatR;
using System.Collections.Generic;
using System;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterTodosSolicitacaoCompra
{
    public class ObterSolicitacaoCompraQuery : IRequest<IEnumerable<Guid>>
    {
    }
}
