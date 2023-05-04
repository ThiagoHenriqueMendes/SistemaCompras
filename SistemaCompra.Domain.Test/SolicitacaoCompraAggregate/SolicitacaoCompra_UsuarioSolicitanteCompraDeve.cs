using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using Xunit;

namespace SistemaCompra.Domain.Test.SolicitacaoCompraAggregate
{
    public class SolicitacaoCompra_UsuarioSolicitanteCompraDeve
    {
        [Fact]
        public void NotificarErroPorNomeUsuarioSolicitanteInvalido()
        {
            //Quando
            var ex = Assert.Throws<ArgumentNullException>(() => new UsuarioSolicitante(string.Empty));

            //Então
            Assert.IsType(typeof(ArgumentNullException), ex);
        }

        [Theory]
        [InlineData("t")]
        [InlineData("th")]
        [InlineData("thi")]
        [InlineData("thia")]
        public void NotificarErroPorNomeUsuarioSolicitanteMenorQueOPermitido(string usuarioSolicitante)
        {
            //Quando
            var ex = Assert.Throws<BusinessRuleException>(() => new UsuarioSolicitante(usuarioSolicitante));

            //Então
            Assert.Equal("Nome de usuário deve possuir pelo menos 5 caracteres.", ex.Message);
        }
    }
}
