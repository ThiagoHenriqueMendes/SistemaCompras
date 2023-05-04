using MediatR;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class RegistrarCompraCommandHandler : CommandHandler, IRequestHandler<RegistrarCompraCommand, bool>
    {
        private readonly ISolicitacaoCompraRepository _solicitacaoCompraRepository;

        public RegistrarCompraCommandHandler(ISolicitacaoCompraRepository solicitacaoCompraRepository, IUnitOfWork uow, IMediator mediator) : base(uow, mediator)
        {
            _solicitacaoCompraRepository = solicitacaoCompraRepository;
        }

        public Task<bool> Handle(RegistrarCompraCommand requisicao, CancellationToken cancellationToken)
        {
            var solicitacaoCompra = ConstruaObjetoSolicitacaoCompra(requisicao);

            var ListaItemCompra = ConstruaObjetoListaItemCompra(requisicao.ListaItem);

            solicitacaoCompra.RegistrarCompra(ListaItemCompra);

            _solicitacaoCompraRepository.Registrar(solicitacaoCompra);

            Commit();
            PublishEvents(solicitacaoCompra.Events);

            return Task.FromResult(true);

        }

        private SolicitacaoCompraAgg.SolicitacaoCompra ConstruaObjetoSolicitacaoCompra(RegistrarCompraCommand requisicao)
            => new SolicitacaoCompraAgg.SolicitacaoCompra(requisicao.UsuarioSolicitante, requisicao.NomeFornecedor);

        private IEnumerable<Item> ConstruaObjetoListaItemCompra(IEnumerable<ItemCommand> ListaItem)
        {
            if (ListaItem is null)
                return Enumerable.Empty<Item>();

            return ListaItem.Select(item => new Item(ConstruaObjetoProduto(item.Produto), item.Quantidade)).ToList();
        }

        private ProdutoAgg.Produto ConstruaObjetoProduto(ProdutoCommand produtoCommand)
           => new ProdutoAgg.Produto(produtoCommand.Nome, produtoCommand.Descricao, produtoCommand.Categoria, produtoCommand.Preco);
    }
}
