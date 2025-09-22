using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly List<User> Users = new();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll() => Ok(Users);

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public IActionResult Create(UserDto dto)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(dto);
            if (!Validator.TryValidateObject(dto, context, validationResults, true))
                return BadRequest(validationResults);

            var user = new User { Id = nextId++, Name = dto.Name, Email = dto.Email };
            Users.Add(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UserDto dto)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user is null) return NotFound();

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(dto);
            if (!Validator.TryValidateObject(dto, context, validationResults, true))
                return BadRequest(validationResults);

            user.Name = dto.Name;
            user.Email = dto.Email;
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user is null) return NotFound();

            Users.Remove(user);
            return NoContent();
        }
    }

    public record User
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public record UserDto
    {
        [Required] public string Name { get; set; } = default!;
        [Required, EmailAddress] public string Email { get; set; } = default!;
    }
}
