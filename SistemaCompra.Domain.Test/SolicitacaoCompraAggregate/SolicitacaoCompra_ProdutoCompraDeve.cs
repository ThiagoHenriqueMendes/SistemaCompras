using Bogus;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.ProdutoAggregate;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using Xunit.Abstractions;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_ProdutoCompraDeve
    {
        private readonly Faker _faker;
        public SolicitacaoCompra_ProdutoCompraDeve()
        {
            _faker = new Faker();
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarNomeProduto()
        {
            //Quando 
            var ex = Assert.Throws<ArgumentNullException>(() => new Produto(null, "Eucalipto", Categoria.Madeira.ToString(), _faker.Random.Decimal()));
            
            //Então
            Assert.IsType(typeof(ArgumentNullException), ex);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarDescricaoProduto()
        {
            //Quando 
            var ex = Assert.Throws<ArgumentNullException>(() => new Produto("Eucalipto", null, Categoria.Madeira.ToString(), _faker.Random.Decimal()));

            //Então
            Assert.IsType(typeof(ArgumentNullException), ex);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ABC")]
        [InlineData("ASFSAFDSAFS")]
        [InlineData("!#@@#!@#")]
        public void NotificarErroQuandoInformarCategoriaInvalidaProdutos(string categoria)
        {
            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => new Produto("Eucalipto", "Eucalipto", categoria, _faker.Random.Decimal()));

            //Então
            Assert.Equal("Categoria informado no produto inválido.", ex.Message);
        }

        [Fact]
        public void NotificarErroQuandoNaoInformarCategoriaProduto()
        {
            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => new Produto("Eucalipto", "Eucalipto", null, _faker.Random.Decimal()));

            //Então
            Assert.Equal("Categoria informado no produto inválido.", ex.Message);
        }

        [Fact]
        public void NotificarErroQuandoAtualizarPrecoComProdutoDesativadoProduto()
        {
            //Dado
            var produto = new Produto("Eucalipto", "Eucalipto", Categoria.Madeira.ToString(), _faker.Random.Decimal(0.1M, 50000));
            produto.Suspender();
            
            //Quando 
            var ex = Assert.Throws<BusinessRuleException>(() => produto.AtualizarPreco(_faker.Random.Decimal(0.1M, 50000)));

            //Então
            Assert.Equal("Produto deve estar ativo!", ex.Message);
            Assert.Equal(produto.Situacao, Domain.ProdutoAggregate.Situacao.Suspenso);
        }
    }
}
