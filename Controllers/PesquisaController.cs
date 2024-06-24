using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PesquisaController : ControllerBase
    {
        private readonly PesquisaService _pesquisaService;

        public PesquisaController(PesquisaService pesquisaService)
        {
            _pesquisaService = pesquisaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pesquisa>>> GetAll()
        {
            var pesquisas = await _pesquisaService.GetAll();
            return Ok(pesquisas);
        }

        [HttpGet("{id}", Name = "GetPesquisa")]
        public async Task<ActionResult<Pesquisa>> GetById(string id)
        {
            var pesquisa = await _pesquisaService.GetById(id);

            if (pesquisa == null)
            {
                return NotFound();
            }

            return Ok(pesquisa);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Pesquisa>>> GetByUserId(string userId)
        {
            var pesquisas = await _pesquisaService.GetByUserId(userId);
            return Ok(pesquisas);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Pesquisa pesquisa)
        {
            pesquisa.DataHora = DateTime.UtcNow; // Garantir que a data e hora sejam definidas como UTC no momento da criação
            await _pesquisaService.Create(pesquisa);
            return StatusCode(201); // Retorna 201 Created sem gerar uma URL
        }
    }
}
