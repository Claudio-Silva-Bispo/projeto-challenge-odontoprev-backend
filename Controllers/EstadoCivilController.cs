using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoCivilController : ControllerBase
    {
        private readonly IEstadoCivilService _estadoCivilService;

        public EstadoCivilController(IEstadoCivilService estadoCivilService)
        {
            _estadoCivilService = estadoCivilService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EstadoCivil>>> GetAll()
        {
            var estadosCivis = await _estadoCivilService.GetAll();
            return Ok(estadosCivis);
        }

        [HttpGet("{id:length(24)}", Name = "GetEstadoCivil")]
        public async Task<ActionResult<EstadoCivil>> GetById(string id)
        {
            var estadoCivil = await _estadoCivilService.GetById(id);

            if (estadoCivil == null)
            {
                return NotFound();
            }

            return Ok(estadoCivil);
        }

        [HttpPost]
        public async Task<ActionResult<EstadoCivil>> Create(EstadoCivil estadoCivil)
        {
            await _estadoCivilService.Create(estadoCivil);
            return CreatedAtRoute("GetEstadoCivil", new { id = estadoCivil.id_estado_civil }, estadoCivil);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, EstadoCivil estadoCivil)
        {
            var existingEstadoCivil = await _estadoCivilService.GetById(id);

            if (existingEstadoCivil == null)
            {
                return NotFound();
            }

            await _estadoCivilService.Update(id, estadoCivil);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingEstadoCivil = await _estadoCivilService.GetById(id);

            if (existingEstadoCivil == null)
            {
                return NotFound();
            }

            await _estadoCivilService.Delete(id);
            return NoContent();
        }
    }
}
