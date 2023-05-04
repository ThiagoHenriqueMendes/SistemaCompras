using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using System;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("compra/registrar")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult RegistrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            try
            {
                var teste = _mediator.Send(registrarCompraCommand);
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                return StatusCode(201);
            }
        }
    }
}
