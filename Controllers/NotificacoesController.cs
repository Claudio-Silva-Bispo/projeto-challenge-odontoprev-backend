using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacoesController : ControllerBase
    {
        private readonly INotificacoesService _notificacoesService;

        public NotificacoesController(INotificacoesService notificacoesService)
        {
            _notificacoesService = notificacoesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Notificacoes>>> GetAll()
        {
            var notificacoes = await _notificacoesService.GetAll();
            return Ok(notificacoes);
        }

        [HttpGet("{id:length(24)}", Name = "GetNotificacoes")]
        public async Task<ActionResult<Notificacoes>> GetById(string id)
        {
            var notificacao = await _notificacoesService.GetById(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            return Ok(notificacao);
        }

        [HttpPost]
        public async Task<ActionResult<Notificacoes>> Create(Notificacoes notificacoes)
        {
            await _notificacoesService.Create(notificacoes);
            return CreatedAtRoute("GetNotificacoes", new { id = notificacoes.id_notificacoes }, notificacoes);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Notificacoes notificacoes)
        {
            var existingNotificacao = await _notificacoesService.GetById(id);

            if (existingNotificacao == null)
            {
                return NotFound();
            }

            await _notificacoesService.Update(id, notificacoes);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingNotificacao = await _notificacoesService.GetById(id);

            if (existingNotificacao == null)
            {
                return NotFound();
            }

            await _notificacoesService.Delete(id);
            return NoContent();
        }
    }
}
