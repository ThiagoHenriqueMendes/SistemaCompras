using Microsoft.EntityFrameworkCore;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using SolicitaCompraAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data.SolicitacaoCompra
{
    public class SolicitacaoCompraRepository : ISolicitacaoCompraRepository
    {
        private readonly SistemaCompraContext _context;

        public SolicitacaoCompraRepository(SistemaCompraContext context)
        {
            _context = context;
        }

        public void Registrar(SolicitaCompraAgg.SolicitacaoCompra entity)
        {
            _context.Set<SolicitaCompraAgg.SolicitacaoCompra>().Add(entity);
        }

        public SolicitaCompraAgg.SolicitacaoCompra Obter(Guid id)
            => _context.SolicitacaoCompras.AsNoTracking().Include(x => x.Itens).ThenInclude(y => y.Produto).Where(c => c.Id == id).FirstOrDefault();

        public IEnumerable<Guid> ObterTodos()
            => _context.SolicitacaoCompras.AsNoTracking().Select(x=> x.Id).ToList();
    }
}