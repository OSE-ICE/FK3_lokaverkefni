using FK3_lokaverkefni.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FK3_lokaverkefni.Controllers
{

    [Route("api/events")]
    [Controller]
    public class EventsController : ControllerBase
    {
        private readonly IRepository _repository;

        public EventsController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Event>>> GetAllEvents()
        {
            try
            {
                List<Models.Event> events = await _repository.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Models.Event>> GetEventById(int id)
        {
            try
            {
                List<Models.Event> events = await _repository.GetAllEventsAsync();
                Models.Event ev = events.FirstOrDefault(e => e.EventId == id);
                if (ev == null)
                {
                    return NotFound();
                }
                return Ok(ev);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Models.Event>> AddEvent([FromBody] Models.Event ev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddEventAsync(ev);
                    return CreatedAtAction(nameof(GetEventById), new { id = ev.EventId }, ev);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Models.Event>> UpdateEvent(int id, [FromBody] Models.Event ev)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Models.Event updatedEvent = await _repository.UpdateEventAsync(id, ev);
                    if (updatedEvent == null)
                    {
                        return NotFound();
                    }
                    return Ok(updatedEvent);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                bool deleted = await _repository.DeleteEventAsync(id);
                if (deleted)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

    }
}
