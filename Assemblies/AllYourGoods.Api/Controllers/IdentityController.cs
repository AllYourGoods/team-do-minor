using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AllYourGoods.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;
using AllYourGoods.Api.Data;

namespace AllYourGoods.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    private readonly ApplicationContext _context;

    public AuthController(UserManager<User> userManager, ApplicationContext context, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _context = context;
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
            var refreshToken = await RefreshTokenPipeline(user);
            return Ok(new { token, refreshToken });
        }
        else if (user != null) 
        {
            await _userManager.AccessFailedAsync(user);
        }
        return Unauthorized();
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

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(string refreshToken) {
        RefreshToken token = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);
        
        if(token != null && token.ExpirationDate > DateTime.Now ) 
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == token.UserFK);
            var newToken = await GenerateJwtToken(user);
            return Ok( new { newToken } );
        }   
        
        return Unauthorized();
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

    private async Task<string> RefreshTokenPipeline(User user) {
        var newToken = await GenerateRefreshToken();
        await CleanupRefreshToken(user);

        _context.RefreshTokens.Add( new RefreshToken { 
            Id = Guid.NewGuid().ToString(), 
            Token = newToken,
            ExpirationDate = DateTime.Now.AddDays(7),
            User = user
        });

        await _context.SaveChangesAsync();

        return newToken;
    }

    private async Task CleanupRefreshToken(User user) {
        RefreshToken? oldToken = _context.RefreshTokens.FirstOrDefault(rt => rt.UserFK == user.Id);
        
        if(oldToken != null) {
            _context.RefreshTokens.Remove(oldToken);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<string> GenerateRefreshToken() {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
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

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("Invalid email address.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Just return the token for now, this should be sent through mail later on
        return Ok(new { ResetToken = token });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("Invalid email address.");
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

        if (result.Succeeded)
        {
            return Ok("Password reset successfully.");
        }

        return BadRequest(result.Errors);
    }
}
