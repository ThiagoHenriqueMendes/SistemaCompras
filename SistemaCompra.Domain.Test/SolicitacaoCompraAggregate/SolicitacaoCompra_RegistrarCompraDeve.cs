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
    public class SolicitacaoCompra_RegistrarCompraDeve
    {
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
        public void NotificarErroPorNomeUsuarioSolicitanteInvalido()
        {
            //Quando
            var ex = Assert.Throws<ArgumentNullException>(() => new SolicitacaoCompra(string.Empty, "João Teste Fornecedor"));
           
            //Então
            Assert.IsType(typeof(ArgumentNullException),ex);
        }

        [Theory]
        [InlineData("t")]
        [InlineData("th")]
        [InlineData("thi")]
        [InlineData("thia")]
        public void NotificarErroPorNomeUsuarioSolicitanteMenorQueOPermitido(string usuarioSolicitante)
        {
            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => new SolicitacaoCompra(usuarioSolicitante, "João Teste Fornecedor"));

            //Então
            Assert.Equal("Nome de usuário deve possuir pelo menos 5 caracteres.", ex.Message);
        }

        [Fact]
        public void NotificarErroPorNomeFornecedorInvalido()
        {
            //Quando
            var ex = Assert.Throws<ArgumentNullException>(() => new SolicitacaoCompra("Thiago Henrique", string.Empty));
           
            //Então
            Assert.IsType(typeof(ArgumentNullException), ex);
        }

        [Theory]
        [InlineData("J")]
        [InlineData("Jo")]
        [InlineData("Joã")]
        [InlineData("João")]
        [InlineData("João ")]
        [InlineData("João C")]
        [InlineData("João Co")]
        public void NotificarErroPorNomeFornecedorMenorQueOPermitido(string nomeFornecedor)
        {
            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => new SolicitacaoCompra("Thiago Henrique", nomeFornecedor));

            //Então
            Assert.Equal("Nome de fornecedor deve ter pelo menos 10 caracteres.", ex.Message);
        }
    }
}
