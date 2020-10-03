using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using NilDevStudio.Domain.Identity;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using NilDevStudio.WebAPI.DTO;
using System;

namespace NilDevStudio.WebAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
    {
		private readonly IConfiguration _config;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IMapper _mapper;

		public UserController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
		{
			_signInManager = signInManager;
			_mapper = mapper;
			_config = config;
			_userManager = userManager;
		}

		[HttpGet("GetUser")]
		public async Task<IActionResult> GetUser()
		{
			return Ok(new UserDTO());
		}

		[HttpPost("Register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register(UserDTO userDto)
		{
			try
			{
				var user = _mapper.Map<User>(userDto);
				var result = await _userManager.CreateAsync(user, userDto.Password);
				var userToReturn = _mapper.Map<UserDTO>(user);

				if(result.Succeeded)
				{
					return Created("GetUser", userToReturn);
				}

				return BadRequest(result.Errors);
			}
			catch (System.Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error occurred: {ex.Message}");
			}
		}

		[HttpPost("Login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login(UserLoginDTO userLogin)
		{
			try
			{
				var user = await _userManager.FindByNameAsync(userLogin.UserName);
				var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

				if(result.Succeeded)
				{
					var appUser = await _userManager.Users
						.FirstOrDefaultAsync(u => u.NormalizedUserName == userLogin.UserName.ToUpper());

					var userToReturn = _mapper.Map<UserLoginDTO>(appUser);

					return Ok(new {
						token = GenerateJWToken(appUser).Result,
						user = userToReturn
					});
				}

				return Unauthorized();
			}
			catch (System.Exception ex)
			{
				return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database error {ex.Message}");
			}
		}

		private async Task<string> GenerateJWToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName)
			};

			var roles = await _userManager.GetRolesAsync(user);

			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var key = new SymmetricSecurityKey(Encoding.ASCII
				.GetBytes(_config.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

    }
}
