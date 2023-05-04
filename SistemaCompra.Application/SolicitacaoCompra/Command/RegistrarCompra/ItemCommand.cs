namespace SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra
{
    public class ItemCommand
    {
        public ProdutoCommand Produto { get; set; }

        public int Quantidade { get; set; }
    }
}
