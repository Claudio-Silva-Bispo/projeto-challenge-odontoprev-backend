using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> GetAll()
        {
            var feedbacks = await _feedbackService.GetAll();
            return Ok(feedbacks);
        }

        [HttpGet("{id:length(24)}", Name = "GetFeedback")]
        public async Task<ActionResult<Feedback>> GetById(string id)
        {
            var feedback = await _feedbackService.GetById(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> Create(Feedback feedback)
        {
            await _feedbackService.Create(feedback);
            return CreatedAtRoute("GetFeedback", new { id = feedback.id_feedback }, feedback);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Feedback feedback)
        {
            var existingFeedback = await _feedbackService.GetById(id);

            if (existingFeedback == null)
            {
                return NotFound();
            }

            await _feedbackService.Update(id, feedback);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingFeedback = await _feedbackService.GetById(id);

            if (existingFeedback == null)
            {
                return NotFound();
            }

            await _feedbackService.Delete(id);
            return NoContent();
        }
    }
}
