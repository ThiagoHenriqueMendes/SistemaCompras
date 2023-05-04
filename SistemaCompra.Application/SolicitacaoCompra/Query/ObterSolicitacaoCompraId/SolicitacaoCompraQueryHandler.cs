using AutoMapper;
using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaCompra.Application.SolicitacaoCompra.Query.ObterSolicitacaoCompraId
{
    public class SolicitacaoCompraQueryHandler : IRequestHandler<ObterSolicitacaoCompraIDQuery, SolicitacaoCompraIdViewModel>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;
        private readonly IMapper _mapper;

        public SolicitacaoCompraQueryHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IMapper mapper)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
            _mapper = mapper;
        }
        public Task<SolicitacaoCompraIdViewModel> Handle(ObterSolicitacaoCompraIDQuery request, CancellationToken cancellationToken)
        {
            var compra = _solicitacaoCompraRepository.Obter(request.Id);
            var produtoViewModel = _mapper.Map<SolicitacaoCompraIdViewModel>(compra);

            return Task.FromResult(produtoViewModel);
        }
    }
}
