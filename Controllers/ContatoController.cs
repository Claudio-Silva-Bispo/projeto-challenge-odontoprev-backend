using UserApi.Models;
using UserApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly ContatoService _contatoService;

        public ContatoController(ContatoService contatoService) =>
            _contatoService = contatoService;

        [HttpGet]
        public async Task<ActionResult<List<Contato>>> Get() =>
            Ok(await _contatoService.GetContatoAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contato newContato)
        {
            if (newContato == null)
            {
                return BadRequest("Contato is null.");
            }

            await _contatoService.CreateContatoAsync(newContato);

            return CreatedAtAction(nameof(Get), new { id = newContato.Id }, newContato);
        }
    }
}
