using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _cadastroService;

        public CadastroController(ICadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cadastro>>> GetAll()
        {
            var cadastros = await _cadastroService.GetAll();
            return Ok(cadastros);
        }

        [HttpGet("{id:length(24)}", Name = "GetCadastro")]
        public async Task<ActionResult<Cadastro>> GetById(string id)
        {
            var cadastro = await _cadastroService.GetById(id);

            if (cadastro == null)
            {
                return NotFound();
            }

            return Ok(cadastro);
        }

        [HttpPost]
        public async Task<ActionResult<Cadastro>> Create(Cadastro cadastro)
        {
            await _cadastroService.Create(cadastro);

            return CreatedAtRoute("GetCadastro", new { id = cadastro.Id }, cadastro);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cadastro cadastro)
        {
            var existingCadastro = await _cadastroService.GetById(id);

            if (existingCadastro == null)
            {
                return NotFound();
            }

            await _cadastroService.Update(id, cadastro);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingCadastro = await _cadastroService.GetById(id);

            if (existingCadastro == null)
            {
                return NotFound();
            }

            await _cadastroService.Delete(id);

            return NoContent();
        }
    }
}
