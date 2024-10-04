using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoNotificacaoController : ControllerBase
    {
        private readonly ITipoNotificacaoService _tipoNotificacaoService;

        public TipoNotificacaoController(ITipoNotificacaoService tipoNotificacaoService)
        {
            _tipoNotificacaoService = tipoNotificacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoNotificacao>>> GetAll()
        {
            var tiposNotificacao = await _tipoNotificacaoService.GetAll();
            return Ok(tiposNotificacao);
        }

        [HttpGet("{id:length(24)}", Name = "GetTipoNotificacao")]
        public async Task<ActionResult<TipoNotificacao>> GetById(string id)
        {
            var tipoNotificacao = await _tipoNotificacaoService.GetById(id);

            if (tipoNotificacao == null)
            {
                return NotFound();
            }

            return Ok(tipoNotificacao);
        }

        [HttpPost]
        public async Task<ActionResult<TipoNotificacao>> Create(TipoNotificacao tipoNotificacao)
        {
            await _tipoNotificacaoService.Create(tipoNotificacao);
            return CreatedAtRoute("GetTipoNotificacao", new { id = tipoNotificacao.id_tipo_notificacao }, tipoNotificacao);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TipoNotificacao tipoNotificacao)
        {
            var existingTipoNotificacao = await _tipoNotificacaoService.GetById(id);

            if (existingTipoNotificacao == null)
            {
                return NotFound();
            }

            await _tipoNotificacaoService.Update(id, tipoNotificacao);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTipoNotificacao = await _tipoNotificacaoService.GetById(id);

            if (existingTipoNotificacao == null)
            {
                return NotFound();
            }

            await _tipoNotificacaoService.Delete(id);
            return NoContent();
        }
    }
}
