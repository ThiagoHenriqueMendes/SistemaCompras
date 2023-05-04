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
    }
}
