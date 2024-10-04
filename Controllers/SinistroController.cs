using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SinistroController : ControllerBase
    {
        private readonly ISinistroService _sinistroService;

        public SinistroController(ISinistroService sinistroService)
        {
            _sinistroService = sinistroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sinistro>>> GetAll()
        {
            var sinistros = await _sinistroService.GetAll();
            return Ok(sinistros);
        }

        [HttpGet("{id:length(24)}", Name = "GetSinistro")]
        public async Task<ActionResult<Sinistro>> GetById(string id)
        {
            var sinistro = await _sinistroService.GetById(id);

            if (sinistro == null)
            {
                return NotFound();
            }

            return Ok(sinistro);
        }

        [HttpPost]
        public async Task<ActionResult<Sinistro>> Create(Sinistro sinistro)
        {
            await _sinistroService.Create(sinistro);
            return CreatedAtRoute("GetSinistro", new { id = sinistro.id_sinistro }, sinistro);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Sinistro sinistro)
        {
            var existingSinistro = await _sinistroService.GetById(id);

            if (existingSinistro == null)
            {
                return NotFound();
            }

            await _sinistroService.Update(id, sinistro);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingSinistro = await _sinistroService.GetById(id);

            if (existingSinistro == null)
            {
                return NotFound();
            }

            await _sinistroService.Delete(id);
            return NoContent();
        }
    }
}
