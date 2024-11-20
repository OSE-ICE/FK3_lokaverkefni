using FK3_lokaverkefni.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FK3_lokaverkefni.Models;

namespace FK3_lokaverkefni.Controllers
{

    [Route("api/routes")]
    [Controller]
    public class RoutesController : ControllerBase
    {
        private readonly IRepository _repository;

        public RoutesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.Route>>> GetAllRoutes()
        {
            try
            {
                List<Models.Route> routes = await _repository.GetAllRoutesAsync();
                return Ok(routes);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Models.Route>> GetRouteById(int id)
        {
            try
            {
                List<Models.Route> routes = await _repository.GetAllRoutesAsync();
                Models.Route route = routes.FirstOrDefault(r => r.RouteId == id);
                if (route == null)
                {
                    return NotFound();
                }
                return Ok(route);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Models.Route>> AddRoute([FromBody] Models.Route route)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddRouteAsync(route);
                    return CreatedAtAction(nameof(GetRouteById), new { id = route.RouteId }, route);
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
        public async Task<ActionResult<Models.Route>> UpdateRoute(int id, [FromBody] Models.Route route)
        {
            try
            {
                Models.Route rout = await _repository.UpdateRouteAsync(id, route);
                if (rout == null)
                {
                    return NotFound();
                }
                else
                { 
                    return CreatedAtAction(nameof(GetRouteById), new { id = route.RouteId }, rout); 
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Models.Route>> DeleteRoute(int id)
        {
            try
            {
               bool success = await _repository.DeleteRouteAsync(id);
                if (success)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
