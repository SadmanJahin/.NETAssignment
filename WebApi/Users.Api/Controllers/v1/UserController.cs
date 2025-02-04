using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Validation;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
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
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("count")]
        public async Task<IActionResult> CountUser(PageRequest request)
        {
            try
            {
                PropertyValidator validator = new PropertyValidator();
                var result = validator.Validate(request);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(item => item.ErrorMessage));
                }
                var response = await _userService.CountUsersAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchUser(PageRequest request)
        {
            try
            {
                PropertyValidator validator = new PropertyValidator();
                var result = validator.Validate(request);
                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(item=> item.ErrorMessage));
                }

                var response = await _userService.SearchUsersAsync(request);
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
