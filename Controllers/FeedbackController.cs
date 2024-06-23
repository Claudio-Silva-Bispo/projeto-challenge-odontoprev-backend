using UserApi.Models;
using UserApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackService feedbackService) =>
            _feedbackService = feedbackService;

        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> Get() =>
            Ok(await _feedbackService.GetFeedbacksAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Feedback newFeedback)
        {
            if (newFeedback == null)
            {
                return BadRequest("Feedback is null.");
            }

            await _feedbackService.CreateFeedbackAsync(newFeedback);

            return CreatedAtAction(nameof(Get), new { id = newFeedback.Id }, newFeedback);
        }
    }
}
