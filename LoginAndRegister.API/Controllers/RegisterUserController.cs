using LoginAndRegister.API.DatabaseContext;
using LoginAndRegister.API.Models;
using LoginAndRegister.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RegisterUserController> _logger;
        private readonly UserManager<SystemUser> _userManager;

        public RegisterUserController(ApplicationDbContext dbContext,
            ILogger<RegisterUserController> logger,
            UserManager<SystemUser> userManager
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("StandardUser")]
        public async Task<IActionResult> RegisterStandardUser([FromBody] RegsiterUserDTO registerUserDto)
        {
            try
            {
                var systemUser = new SystemUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = registerUserDto.Name,
                    Address = registerUserDto.Address,
                    Phone = registerUserDto.Phone,
                    Email = registerUserDto.Email.ToString(),
                    UserName = registerUserDto.Email
                };
                var standardUserCreationResult = await _userManager.CreateAsync(user: systemUser, password: registerUserDto.Password);
                if(!standardUserCreationResult.Succeeded)
                {
                    var errors = standardUserCreationResult.Errors.Select(e => e.Description).ToList();
                    _logger.LogError("[RegisterStandardUser] Unable to register the new user. Errors: {ErrorList}", string.Join(",", errors));
                    return BadRequest(errors);
                }
                return CreatedAtAction(nameof(RegisterStandardUser), standardUserCreationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RegisterStandardUser] An unexpected error occured: {ErrorMessage}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "An unexpected error occurred",
                    Detail = ex.Message,
                });
            }
        }
    }
}
