using Tarea3.Models;
using Tarea3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Tarea3.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { message = "Datos inválidos.", errors });
            }

            var (succeeded, errorMessage) = await _accountService.LoginAsync(
                model.Email, model.Password, model.RememberMe);

            if (!succeeded)
                return Unauthorized(new { message = errorMessage });

            return Ok(new { message = "Login exitoso." });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return BadRequest(new { message = "Datos inválidos.", errors = modelErrors });
            }

            var (succeeded, errors) = await _accountService.RegisterAsync(model);

            if (!succeeded)
                return BadRequest(new { message = "Error al registrar.", errors });

            return Ok(new { message = "Registro exitoso." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return Ok(new { message = "Sesión cerrada." });
        }
    }
}