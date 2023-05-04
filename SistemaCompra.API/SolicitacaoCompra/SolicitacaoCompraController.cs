using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaCompra.Application.SolicitacaoCompra.Command.RegistrarCompra;
using SistemaCompra.Application.SolicitacaoCompra.Query;
using SistemaCompra.Domain.Core;
using System;
using System.Threading.Tasks;

namespace SistemaCompra.API.SolicitacaoCompra
{
    public class SolicitacaoCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SolicitacaoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("compra/{id}")]
        public IActionResult Obter(Guid id)
        {
            var produtoViewModel = _mediator.Send(new ObterSolicitacaoCompraQuery(id));
            return Ok(produtoViewModel);
        }

        [HttpPost, Route("compra/registrar")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> RegistrarCompra([FromBody] RegistrarCompraCommand registrarCompraCommand)
        {
            try
            {
                var response = await _mediator.Send(registrarCompraCommand);
                return StatusCode(201);
            }
            catch (BusinessRuleException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



    }
}
