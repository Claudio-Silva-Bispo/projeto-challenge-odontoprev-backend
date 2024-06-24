using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitanteAceiteController : ControllerBase
    {
        private readonly VisitanteAceiteService _visitanteAceiteService;

        public VisitanteAceiteController(VisitanteAceiteService visitanteAceiteService)
        {
            _visitanteAceiteService = visitanteAceiteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VisitanteAceite>>> GetAll()
        {
            var visitantes = await _visitanteAceiteService.GetAll();
            return Ok(visitantes);
        }

        [HttpGet("{id}", Name = "GetVisitanteAceite")]
        public async Task<ActionResult<VisitanteAceite>> GetById(string id)
        {
            var visitante = await _visitanteAceiteService.GetById(id);

            if (visitante == null)
            {
                return NotFound();
            }

            return Ok(visitante);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] VisitanteAceite visitanteAceite)
        {
            await _visitanteAceiteService.Create(visitanteAceite);
            return StatusCode(201); // Retorna 201 Created sem gerar uma URL
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] VisitanteAceite visitanteAceite)
        {
            var existingVisitante = await _visitanteAceiteService.GetById(id);
            if (existingVisitante == null)
            {
                return NotFound();
            }

            await _visitanteAceiteService.Update(id, visitanteAceite);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingVisitante = await _visitanteAceiteService.GetById(id);
            if (existingVisitante == null)
            {
                return NotFound();
            }

            await _visitanteAceiteService.Delete(id);
            return NoContent();
        }
    }
}
