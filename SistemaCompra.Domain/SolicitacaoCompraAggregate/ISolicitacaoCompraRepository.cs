namespace SistemaCompra.Domain.SolicitacaoCompraAggregate
{
    public interface ISolicitacaoCompraRepository
    {
        void Registrar(SolicitacaoCompra solicitacaoCompra);
    }
}
