using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitanteAnonimoController : ControllerBase
    {
        private readonly VisitanteAnonimoService _visitanteAnonimoService;

        public VisitanteAnonimoController(VisitanteAnonimoService visitanteAnonimoService)
        {
            _visitanteAnonimoService = visitanteAnonimoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VisitanteAnonimo>>> GetAll()
        {
            var visitantes = await _visitanteAnonimoService.GetAll();
            return Ok(visitantes);
        }

        [HttpGet("{id}", Name = "GetVisitanteAnonimo")]
        public async Task<ActionResult<VisitanteAnonimo>> GetById(string id)
        {
            var visitante = await _visitanteAnonimoService.GetById(id);

            if (visitante == null)
            {
                return NotFound();
            }

            return Ok(visitante);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VisitanteAnonimo visitanteAnonimo)
        {
            await _visitanteAnonimoService.Create(visitanteAnonimo);
            return StatusCode(201); // Retorna 201 Created sem gerar uma URL
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] VisitanteAnonimo visitanteAnonimo)
        {
            var existingVisitante = await _visitanteAnonimoService.GetById(id);
            if (existingVisitante == null)
            {
                return NotFound();
            }

            await _visitanteAnonimoService.Update(id, visitanteAnonimo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingVisitante = await _visitanteAnonimoService.GetById(id);
            if (existingVisitante == null)
            {
                return NotFound();
            }

            await _visitanteAnonimoService.Delete(id);
            return NoContent();
        }
    }
}
