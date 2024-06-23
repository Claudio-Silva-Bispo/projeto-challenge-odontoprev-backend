using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserApi.Servicos;
using UserApi.Models;

namespace UserApi.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ControladorAutenticacao : ControllerBase
    {
        private readonly IAutenticacaoLoginService _servicoAutenticacao;

        public ControladorAutenticacao(IAutenticacaoLoginService servicoAutenticacao)
        {
            _servicoAutenticacao = servicoAutenticacao;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AutenticacaoLogin modelo, [FromQuery] string tipoLogin)
        {
            var token = await _servicoAutenticacao.Autenticar(modelo.Email, modelo.Senha, tipoLogin);

            if (token == null)
                return Unauthorized();

            return Ok(new { Token = token });
        }
    }
}

public class AutenticacaoLogin
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
}
