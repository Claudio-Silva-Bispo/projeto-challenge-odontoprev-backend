using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetAll()
        {
            var clientes = await _clienteService.GetAll();
            return Ok(clientes);
        }

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public async Task<ActionResult<Cliente>> GetById(string id)
        {
            var cliente = await _clienteService.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Create(Cliente cliente)
        {
            await _clienteService.Create(cliente);
            return CreatedAtRoute("GetCliente", new { id = cliente.id_cliente }, cliente);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cliente cliente)
        {
            var existingCliente = await _clienteService.GetById(id);

            if (existingCliente == null)
            {
                return NotFound();
            }

            await _clienteService.Update(id, cliente);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingCliente = await _clienteService.GetById(id);

            if (existingCliente == null)
            {
                return NotFound();
            }

            await _clienteService.Delete(id);
            return NoContent();
        }
    }
}
