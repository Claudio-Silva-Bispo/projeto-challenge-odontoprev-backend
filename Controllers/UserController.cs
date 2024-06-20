using UserApi.Models;
using UserApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet]
        public async Task<List<Usuario>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Usuario>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user!;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cadastro newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Usuario updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;

            await _userService.UpdateAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var cadastro = await _userService.GetCadastroAsync(loginRequest.Email, loginRequest.Senha);

            if (cadastro == null)
            {
                return Unauthorized(new { message = "Email ou senha incorretos" });
            }

            var user = await _userService.GetAsync(cadastro.Id);

            if (user == null)
            {
                return Unauthorized(new { message = "Usuário não encontrado" });
            }

            // Registrar o login na coleção `Login`
            var login = new Login
            {
                UserId = user.Id,
                LoginDate = DateTime.UtcNow
            };
            await _userService.RecordLoginAsync(login);

            // Exemplo simples de resposta, pode incluir token JWT, etc.
            return Ok(new 
            { 
                Id = user.Id,
                Nome = user.Nome
                // Adicione outros campos conforme necessário
            });
        }
    }
}
