using Application;
using Application.Person.Ports;
using Application.PersonGender.Ports;
using Application.System.Dtos;
using Application.System.Ports;
using Application.User.Dtos;
using Data.SqlServer;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly ISystemManager _systemManager;

        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings,
                              ISystemManager systemManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _systemManager = systemManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SystemDto system)
        {
            system.ApiKey = Utils.EncryptKey(system.Name);
            system.EncryptedApiKey = Utils.EncryptKey(system.ApiKey);

            var res = await _systemManager.CreateSystem(system);

            if (res.Success) return Ok(res.Data);

            return BadRequest();
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(SystemAuthenticationDto authenticationDto)
        {
            var system = await _systemManager.GetSystemById(authenticationDto.Id);

            if (!system.Success)
            {
                if (system.ErrorCode == ErrorCodes.PERSON_TYPE_NOT_FOUND)
                {
                    return NotFound("System not found");

                }
                else
                {
                    return BadRequest($"Error: {system.Message}");
                }
            }

            if (Utils.EncryptKey(authenticationDto.ApiKey) != system.Data.EncryptedApiKey)
            {
                return Unauthorized("Invalid credentials");
            }

            var requestingIP = HttpContext.Connection.RemoteIpAddress.ToString();

            var allowedIPs = system.Data.AllowedIPs.Split(',');

            if (!allowedIPs.Contains(requestingIP))
            {
                return Unauthorized("IP not allowed");
            }

            var token = GerarJwt(system.Data);

            return Ok(new { Token = token });
        }

        private async Task<string> GerarJwt(SystemDto systemDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = systemDto.ApiKey,
                Audience = _appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        #region comments
        //[HttpPost("Register")]
        //public async Task<ActionResult> Register(RegisterUserDto registerUser)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        //    var user = new IdentityUser
        //    {
        //        UserName = registerUser.Username,
        //        EmailConfirmed = true
        //    };

        //    var result = await _userManager.CreateAsync(user, registerUser.Password);

        //    if (!result.Succeeded) return BadRequest(result.Errors);

        //    await _signInManager.SignInAsync(user, false);

        //    return Ok(await GerarJwt(registerUser.Username));
        //}

        //[HttpPost("SignIn")]
        //public async Task<ActionResult> Login(LoginUserDto loginUser)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        //    var result = await _signInManager.PasswordSignInAsync(loginUser.Username, loginUser.Password, false, true);

        //    if (result.Succeeded)
        //    {
        //        return Ok(await GerarJwt(loginUser.Username));
        //    }

        //    return BadRequest("Usuário ou senha inválidos");
        //}

        //private async Task<string> GerarJwt(string username)
        //{
        //    var user = await _userManager.FindByNameAsync(username);

        //    // authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Issuer = _appSettings.Issuer,
        //        Audience = _appSettings.ValidIn,
        //        Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationInHours),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        //}
        #endregion
    }
}
