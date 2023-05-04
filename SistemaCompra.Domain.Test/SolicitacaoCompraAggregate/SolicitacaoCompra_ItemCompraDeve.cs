using Bogus;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_ItemCompraDeve
    {
        private readonly Faker _faker;

        public SolicitacaoCompra_ItemCompraDeve()
        {
            _faker = new Faker();
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarProdutoCompra()
        {
            //Dado
            var itens = new List<Item>();

            //Quando 
            var ex = Assert.Throws<ArgumentNullException>(() => itens.Add(new Item(null, 0)));

            //Então
            Assert.IsType(typeof(ArgumentNullException), ex);
        }

        [Fact]
        public void DefinirCalculoSubTotal()
        {
            //Dado
            var produto = new Produto("Eucalipto", "Eucalipto", Categoria.Madeira.ToString(), _faker.Random.Decimal(0.1M, 50000));
            var itemProduto = new Item(produto, _faker.Random.Int());

            //Quando 
            var calculo = itemProduto.Produto.Preco.Value * itemProduto.Qtde;

            //Então
            Assert.Equal(calculo, itemProduto.Subtotal.Value);
        }
    }
}
