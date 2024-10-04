using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentistaController : ControllerBase
    {
        private readonly IDentistaService _dentistaService;

        public DentistaController(IDentistaService dentistaService)
        {
            _dentistaService = dentistaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Dentista>>> GetAll()
        {
            var dentistas = await _dentistaService.GetAll();
            return Ok(dentistas);
        }

        [HttpGet("{id:length(24)}", Name = "GetDentista")]
        public async Task<ActionResult<Dentista>> GetById(string id)
        {
            var dentista = await _dentistaService.GetById(id);

            if (dentista == null)
            {
                return NotFound();
            }

            return Ok(dentista);
        }

        [HttpPost]
        public async Task<ActionResult<Dentista>> Create(Dentista dentista)
        {
            await _dentistaService.Create(dentista);
            return CreatedAtRoute("GetDentista", new { id = dentista.id_dentista }, dentista);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Dentista dentista)
        {
            var existingDentista = await _dentistaService.GetById(id);

            if (existingDentista == null)
            {
                return NotFound();
            }

            await _dentistaService.Update(id, dentista);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingDentista = await _dentistaService.GetById(id);

            if (existingDentista == null)
            {
                return NotFound();
            }

            await _dentistaService.Delete(id);
            return NoContent();
        }
    }
}
