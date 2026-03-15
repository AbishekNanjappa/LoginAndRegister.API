using LoginAndRegister.API.DatabaseContext;
using LoginAndRegister.API.Models;
using LoginAndRegister.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginAndRegister.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUserController : ControllerBase
    {
        private readonly ILogger<LoginUserController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<SystemUser> _userManager;
        private readonly SignInManager<SystemUser> _signInManager;

        public LoginUserController(ILogger<LoginUserController> logger,
            ApplicationDbContext dbContext,
            UserManager<SystemUser> userManager,
            SignInManager<SystemUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("StandardUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDTO loginUserDto)
        {
            var signInAttempt = await _signInManager.PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, isPersistent: false, lockoutOnFailure: false);
            if(signInAttempt.IsLockedOut)
            {
                _logger.LogWarning("User; {UserName} is locked out. Try again later.", loginUserDto.UserName);
                return StatusCode(StatusCodes.Status423Locked, loginUserDto.UserName);
            }
            if(signInAttempt.IsNotAllowed)
            {
                _logger.LogWarning("User: {UserName} is not allowed to sign in. Please confirm you email.", loginUserDto.UserName);
                return Unauthorized("Email confirmation is required to sign in.");
            }
            if(signInAttempt.RequiresTwoFactor)
            {
                _logger.LogInformation("2 Factor Auth required for login.");
                return StatusCode(StatusCodes.Status428PreconditionRequired,"Two factor authentication required");
            }
            if(!signInAttempt.Succeeded)
            {
                _logger.LogError("Sign-In attempt failed. Please check entered credentials.");
                return NotFound("User not found.");
            }
            return Ok("User logged in successfully");
        }
    }
}
 