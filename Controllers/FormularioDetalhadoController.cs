using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormularioDetalhadoController : ControllerBase
    {
        private readonly IFormularioDetalhadoService _formularioDetalhadoService;

        public FormularioDetalhadoController(IFormularioDetalhadoService formularioDetalhadoService)
        {
            _formularioDetalhadoService = formularioDetalhadoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FormularioDetalhado>>> GetAll()
        {
            var formularios = await _formularioDetalhadoService.GetAll();
            return Ok(formularios);
        }

        [HttpGet("{id:length(24)}", Name = "GetFormularioDetalhado")]
        public async Task<ActionResult<FormularioDetalhado>> GetById(string id)
        {
            var formulario = await _formularioDetalhadoService.GetById(id);

            if (formulario == null)
            {
                return NotFound();
            }

            return Ok(formulario);
        }

        [HttpPost]
        public async Task<ActionResult<FormularioDetalhado>> Create(FormularioDetalhado formularioDetalhado)
        {
            await _formularioDetalhadoService.Create(formularioDetalhado);
            return CreatedAtRoute("GetFormularioDetalhado", new { id = formularioDetalhado.id_formulario }, formularioDetalhado);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, FormularioDetalhado formularioDetalhado)
        {
            var existingFormulario = await _formularioDetalhadoService.GetById(id);

            if (existingFormulario == null)
            {
                return NotFound();
            }

            await _formularioDetalhadoService.Update(id, formularioDetalhado);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingFormulario = await _formularioDetalhadoService.GetById(id);

            if (existingFormulario == null)
            {
                return NotFound();
            }

            await _formularioDetalhadoService.Delete(id);
            return NoContent();
        }
    }
}
