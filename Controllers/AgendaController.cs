using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Agenda>>> GetAll()
        {
            var agendas = await _agendaService.GetAll();
            return Ok(agendas);
        }

        [HttpGet("{id:length(24)}", Name = "GetAgenda")]
        public async Task<ActionResult<Agenda>> GetById(string id)
        {
            var agenda = await _agendaService.GetById(id);

            if (agenda == null)
            {
                return NotFound();
            }

            return Ok(agenda);
        }

        [HttpPost]
        public async Task<ActionResult<Agenda>> Create(Agenda agenda)
        {
            await _agendaService.Create(agenda);
            return CreatedAtRoute("GetAgenda", new { id = agenda.id_agenda }, agenda);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Agenda agenda)
        {
            var existingAgenda = await _agendaService.GetById(id);

            if (existingAgenda == null)
            {
                return NotFound();
            }

            await _agendaService.Update(id, agenda);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingAgenda = await _agendaService.GetById(id);

            if (existingAgenda == null)
            {
                return NotFound();
            }

            await _agendaService.Delete(id);
            return NoContent();
        }
    }
}
