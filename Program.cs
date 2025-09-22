using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });
});

var app = builder.Build();

// Middleware: Logging
app.Use(async (context, next) =>
{
    Console.WriteLine($"{DateTime.Now}: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});

// Enable Swagger for any environment
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
});

app.UseHttpsRedirection();

// In-memory store
var users = new List<User>();
var nextId = 1;

// CRUD Endpoints

app.MapGet("/users", () => users);

app.MapGet("/users/{id:int}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/users", (UserDto dto) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(dto);
    if (!Validator.TryValidateObject(dto, context, validationResults, true))
        return Results.BadRequest(validationResults);

    var user = new User { Id = nextId++, Name = dto.Name, Email = dto.Email };
    users.Add(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id:int}", (int id, UserDto dto) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound();

    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(dto);
    if (!Validator.TryValidateObject(dto, context, validationResults, true))
        return Results.BadRequest(validationResults);

    user.Name = dto.Name;
    user.Email = dto.Email;
    return Results.Ok(user);
});

app.MapDelete("/users/{id:int}", (int id) =>
{
    var user = users.FirstOrDefault(u => u.Id == id);
    if (user is null) return Results.NotFound();
    users.Remove(user);
    return Results.NoContent();
});

app.Run();

// Models
record User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}

record UserDto
{
    [Required] public string Name { get; set; } = default!;
    [Required, EmailAddress] public string Email { get; set; } = default!;
}
