using DockerTestApi.Dto;
using DockerTestApi.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DockerTestApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(AppDbContext db) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req)
    {
        if (await db.Users.AnyAsync(u => u.Email == req.Email))
            return Conflict("Bu email zaten kayitli");

        db.Users.Add(new User
        {
            Name = req.Name,
            Surname = req.Surname,
            Email = req.Email,
            Password =  req.Password,
        });
        await db.SaveChangesAsync();

        return Ok("Kullanici kaydi basarili");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);

        if (user is null || req.Password != user.Password)
            return Unauthorized("Email veya sifre hatali");

        return Ok("Giris basarili");
    }
}