﻿using SistemaCompra.Domain.SolicitacaoCompraAggregate;
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

        public void RegistrarCompra(SolicitaCompraAgg.SolicitacaoCompra entity)
        {
            _context.Set<SolicitaCompraAgg.SolicitacaoCompra>().Add(entity);
        }
    }
}
