using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Contracts;
using Users.Core.Entities;
using WebApi.Shared.Models;

namespace Users.Api.Controllers.v1
{

    [ApiVersion(1.0)]
    [Route("api/v{v:apiVersion}/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto user)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (Exception)
            {

            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUserAsync(id);

            return NoContent();
        }

        [HttpPost("count")]
        public async Task<IActionResult> CountUser(PageRequest request)
        {
            var response = await _userService.CountUsersAsync(request);
            return Ok(response);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchUser(PageRequest request)
        {
            var response = await _userService.SearchUsersAsync(request);
            return Ok(response);
        }
    }
}
