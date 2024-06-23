using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalizacaoUsuarioController : ControllerBase
    {
        private readonly PersonalizacaoUsuarioService _personalizacaoService;

        public PersonalizacaoUsuarioController(PersonalizacaoUsuarioService personalizacaoService)
        {
            _personalizacaoService = personalizacaoService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<PersonalizacaoUsuario>> GetByUserId(string userId)
        {
            var personalizacao = await _personalizacaoService.GetByUserIdAsync(userId);

            if (personalizacao == null)
            {
                return NotFound();
            }

            return personalizacao;
        }

        [HttpPost]
        public async Task<ActionResult<PersonalizacaoUsuario>> Create(PersonalizacaoUsuario personalizacao)
        {
            await _personalizacaoService.CreateAsync(personalizacao);

            return CreatedAtAction(nameof(GetByUserId), new { userId = personalizacao.UserId }, personalizacao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, PersonalizacaoUsuario personalizacao)
        {
            var existingPersonalizacao = await _personalizacaoService.GetByUserIdAsync(personalizacao.UserId);

            if (existingPersonalizacao == null || existingPersonalizacao.Id != id)
            {
                return NotFound();
            }

            await _personalizacaoService.UpdateAsync(id, personalizacao);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var personalizacao = await _personalizacaoService.GetByUserIdAsync(id);

            if (personalizacao == null)
            {
                return NotFound();
            }

            await _personalizacaoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
