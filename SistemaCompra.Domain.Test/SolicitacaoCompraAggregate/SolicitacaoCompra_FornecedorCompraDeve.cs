using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_FornecedorCompraDeve
    {
        [Fact]
        public void NotificarErroPorNomeFornecedorInvalido()
        {
            //Quando
            var ex = Assert.Throws<ArgumentNullException>(() => new NomeFornecedor( string.Empty));

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
            var ex = Assert.Throws<BusinessRuleException>(() => new NomeFornecedor( nomeFornecedor));

            //Então
            Assert.Equal("Nome de fornecedor deve ter pelo menos 10 caracteres.", ex.Message);
        }
    }
}
