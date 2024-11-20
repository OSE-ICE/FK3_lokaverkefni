using Microsoft.AspNetCore.Mvc;
using FK3_lokaverkefni.Data.Interfaces;
using FK3_lokaverkefni.Models;
using FK3_lokaverkefni.Models.DTO;

namespace FK3_lokaverkefni.Controllers
{

    [Route("api/users")]
    [Controller]
    public class UsersController : ControllerBase
    {
        private readonly IRepository _repository;
        public UsersController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            try
            {
                List<UserDTO> users = await _repository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                List<UserDTO> users = await _repository.GetAllUsersAsync();
                UserDTO user = users.FirstOrDefault(u => u.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddUserAsync(user);
                    return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
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
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User updatedUser = await _repository.UpdateUserAsync(id, user);
                    if (updatedUser == null)
                    {
                        return NotFound();
                    }
                    return Ok(updatedUser);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                bool deleted = await _repository.DeleteUserAsync(id);
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
