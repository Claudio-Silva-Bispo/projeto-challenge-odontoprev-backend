using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly UserService _userService;

        public FeedbackController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> Get()
        {
            var feedbacks = await _userService.GetFeedbacksAsync();
            return Ok(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Feedback newFeedback)
        {
            if (newFeedback == null)
            {
                return BadRequest("Feedback é nulo.");
            }

            // Garante que o campo Id não está preenchido para permitir a geração automática
            newFeedback.Id = null;
            newFeedback.DataHora = DateTime.UtcNow; // Definir data e hora no servidor

            await _userService.CreateFeedbackAsync(newFeedback);
            return CreatedAtAction(nameof(Get), new { id = newFeedback.Id }, newFeedback);
        }
    }
}
