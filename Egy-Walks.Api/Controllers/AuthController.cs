using Egy_Walks.Api.CustomActionFilter;
using Egy_Walks.Api.Models.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Egy_Walks.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("Register")]
    [ValidateModel]
    public async Task<IActionResult> Register([FromBody] RegisterAddRequest model)
    {
        // Create a new IdentityUser
        var identityUser = new IdentityUser
        {
            UserName = model.Username,
            Email = model.Username,
            EmailConfirmed = true,
        };

        // Attempt to create the user
        var identityResult = await _userManager.CreateAsync(identityUser, model.Password);
        if (identityResult.Succeeded)
        {
            // If roles are provided, add them to the user
            if (model.Roles != null && model.Roles.Any())
            {
                var roleResult = await _userManager.AddToRolesAsync(identityUser, model.Roles);
                if (!roleResult.Succeeded)
                {
                    // If adding roles fails, delete the user and return the role errors
                    await _userManager.DeleteAsync(identityUser);
                    return BadRequest(roleResult.Errors);
                }
            }

            // Return the created user with a 201 status code
            return CreatedAtAction(nameof(GetUser), new { username = identityUser.UserName }, model);
            //return Ok("User created");
        }

        // If user creation failed, return the errors
        return BadRequest(identityResult.Errors);
    }

// Assuming you have a method to get user details
    [HttpGet("{username}")]
    public async Task<IActionResult> GetUser(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        LoginAddRequest response = new LoginAddRequest()
        {
            UserName = user.UserName,
        };
        // Here you should map user to a DTO or return relevant information
        return Ok(response);
    }

    [HttpPost("Login")]
    [ValidateModel]
    public async Task<IActionResult> Login([FromBody] LoginAddRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.UserName);
        if (user != null)
        {
            bool checkPasswordAsync = await _userManager.CheckPasswordAsync(user, model.Password);
            if (checkPasswordAsync)
            {
                return Ok("Signed in Successfully");
            }
        }
        return BadRequest("Invalid username or password");
    }
    
}