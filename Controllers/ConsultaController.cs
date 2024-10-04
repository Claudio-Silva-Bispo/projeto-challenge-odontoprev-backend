using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Consulta>>> GetAll()
        {
            var consultas = await _consultaService.GetAll();
            return Ok(consultas);
        }

        [HttpGet("{id:length(24)}", Name = "GetConsulta")]
        public async Task<ActionResult<Consulta>> GetById(string id)
        {
            var consulta = await _consultaService.GetById(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return Ok(consulta);
        }

        [HttpPost]
        public async Task<ActionResult<Consulta>> Create(Consulta consulta)
        {
            await _consultaService.Create(consulta);
            return CreatedAtRoute("GetConsulta", new { id = consulta.id_consulta }, consulta);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Consulta consulta)
        {
            var existingConsulta = await _consultaService.GetById(id);

            if (existingConsulta == null)
            {
                return NotFound();
            }

            await _consultaService.Update(id, consulta);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingConsulta = await _consultaService.GetById(id);

            if (existingConsulta == null)
            {
                return NotFound();
            }

            await _consultaService.Delete(id);
            return NoContent();
        }
    }
}
