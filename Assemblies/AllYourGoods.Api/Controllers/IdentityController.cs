using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AllYourGoods.Api.Models;

namespace AllYourGoods.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = await GenerateJwtToken(user);
            return Ok(new { token });
        }
        else if (user != null) 
        {
            await _userManager.AccessFailedAsync(user);
        }
        return Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logout successful.");
    }


    [HttpPost("register/group1")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await addUser(model, Roles.group1);

        if (result.Succeeded)
        {
            return Ok(new { Message = "User created successfully!" });
        }

        return BadRequest(result.Errors);
    }


    private async Task<string> GenerateJwtToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user)
        ;
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<IdentityResult> addUser(RegisterModel model, Roles role) {

        var user = new User
        {
            UserName = model.Username,
            Email = model.Email
        };

        var userResult = await _userManager.CreateAsync(user, model.Password);

        if (!userResult.Succeeded) 
        {
            return userResult;
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role.ToString());

        return roleResult;

    }
}
