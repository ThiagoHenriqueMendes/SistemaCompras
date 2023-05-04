using Bogus;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
        private readonly Faker _faker;
        public SolicitacaoCompra_RegistrarCompraDeve()
        {
            _faker = new Faker();
        }


        [Fact]
        public void DefinirPrazo30DiasAoComprarMais50mil()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 1001);
            itens.Add(new Item(produto, 50));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            Assert.Equal(30, solicitacao.CondicaoPagamento.Valor);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarItensCompra()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(itens));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }

        [Fact]
        public void NotificarErroQuandoItensCompraNull()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");

            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => solicitacao.RegistrarCompra(null));

            //Então
            Assert.Equal("A solicitação de compra deve possuir itens!", ex.Message);
        }

        [Fact]
        public void AdicionarItemAListaDeCompraERecalcularTotal()
        {
            //Dado
            var solicitacao = new SolicitacaoCompra("rodrigoasth", "rodrigoasth");
            var itens = new List<Item>();
            var produto = new Produto("Cedro", "Transversal 3/3", Categoria.Madeira.ToString(), 100);
            itens.Add(new Item(produto, _faker.Random.Int(1,100)));

            //Quando
            solicitacao.RegistrarCompra(itens);

            //Então
            var totalGeral = solicitacao.Itens.Sum(x => x.Subtotal.Value);

            Assert.Equal(0, solicitacao.CondicaoPagamento.Valor);
            Assert.Equal(totalGeral, solicitacao.TotalGeral.Value);


            //Quando
            var valorProdutoEucalipto = 50000M;
            var quantiaProdutoEucalipto = _faker.Random.Int(1,5);

            solicitacao.AdicionarItem(new Produto("Eucalipto", "Eucalipto", Categoria.Madeira.ToString(), valorProdutoEucalipto), quantiaProdutoEucalipto);


            totalGeral += (valorProdutoEucalipto * quantiaProdutoEucalipto);

            Assert.Equal(totalGeral, solicitacao.TotalGeral.Value);
        }
    }
}
