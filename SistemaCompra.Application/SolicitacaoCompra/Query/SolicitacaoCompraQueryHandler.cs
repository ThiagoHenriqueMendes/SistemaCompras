using AutoMapper;
using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Query
{
    public class SolicitacaoCompraQueryHandler : IRequestHandler<ObterSolicitacaoCompraQuery, SolicitacaoCompraViewModel>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IMapper _mapper;

        public SolicitacaoCompraQueryHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IMapper mapper)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _mapper = mapper;
        }
        public Task<SolicitacaoCompraViewModel> Handle(ObterSolicitacaoCompraQuery request, CancellationToken cancellationToken)
        {
            var compra = _solicitacaoCompraRepository.Obter(request.Id);
            var produtoViewModel = _mapper.Map<SolicitacaoCompraViewModel>(compra);

            return Task.FromResult(produtoViewModel);
        }
    }
}
