using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Dtos.Auth;
using NzWalks.API.Services.Interfaces;

namespace NzWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded && registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPassword)
                {
                    //generate token....
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        string token = _tokenRepository.CreateToken(user, roles.ToList());
                        var res = new LoginResponseDto { Token = token };
                        return Ok(res);
                    }

                }

            }

            return BadRequest("User Not Found");
        }
    }
}