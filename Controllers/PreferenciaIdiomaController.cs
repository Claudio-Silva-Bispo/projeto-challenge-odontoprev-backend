using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreferenciaIdiomaController : ControllerBase
    {
        private readonly PreferenciaIdiomaService _preferenciaIdiomaService;

        public PreferenciaIdiomaController(PreferenciaIdiomaService preferenciaIdiomaService)
        {
            _preferenciaIdiomaService = preferenciaIdiomaService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<PreferenciaIdioma>> GetByUserId(string userId)
        {
            var preferencia = await _preferenciaIdiomaService.GetByUserId(userId);
            if (preferencia == null)
            {
                return NotFound();
            }

            return Ok(preferencia);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PreferenciaIdioma preferenciaIdioma)
        {
            await _preferenciaIdiomaService.Create(preferenciaIdioma);
            return StatusCode(201); // Retorna 201 Created sem gerar uma URL
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] PreferenciaIdioma preferenciaIdioma)
        {
            var existingPreferencia = await _preferenciaIdiomaService.GetById(id);
            if (existingPreferencia == null)
            {
                return NotFound();
            }

            await _preferenciaIdiomaService.Update(id, preferenciaIdioma);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingPreferencia = await _preferenciaIdiomaService.GetById(id);
            if (existingPreferencia == null)
            {
                return NotFound();
            }

            await _preferenciaIdiomaService.Delete(id);
            return NoContent();
        }
    }
}
