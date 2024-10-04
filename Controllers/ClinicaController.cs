using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicaController : ControllerBase
    {
        private readonly IClinicaService _clinicaService;

        public ClinicaController(IClinicaService clinicaService)
        {
            _clinicaService = clinicaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Clinica>>> GetAll()
        {
            var clinicas = await _clinicaService.GetAll();
            return Ok(clinicas);
        }

        [HttpGet("{id:length(24)}", Name = "GetClinica")]
        public async Task<ActionResult<Clinica>> GetById(string id)
        {
            var clinica = await _clinicaService.GetById(id);

            if (clinica == null)
            {
                return NotFound();
            }

            return Ok(clinica);
        }

        [HttpPost]
        public async Task<ActionResult<Clinica>> Create(Clinica clinica)
        {
            await _clinicaService.Create(clinica);
            return CreatedAtRoute("GetClinica", new { id = clinica.id_clinica }, clinica);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Clinica clinica)
        {
            var existingClinica = await _clinicaService.GetById(id);

            if (existingClinica == null)
            {
                return NotFound();
            }

            await _clinicaService.Update(id, clinica);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingClinica = await _clinicaService.GetById(id);

            if (existingClinica == null)
            {
                return NotFound();
            }

            await _clinicaService.Delete(id);
            return NoContent();
        }
    }
}
