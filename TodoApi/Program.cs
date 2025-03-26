
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MySql.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
    //   "DefaultConnection": "server=localhost;user=root;password=Nn215552936!;database=database"


builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    p => p.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

app.UseCors("AllowAll");


app.Urls.Add("http://localhost:5000");
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// קבלת כל המשימות
app.MapGet("/", async (ToDoDbContext dbContext) =>
{
    var items = await dbContext.Items.ToListAsync();
    return Results.Ok(items);
});

// הוספת משימה חדשה
app.MapPost("/{name}", async (ToDoDbContext dbContext, string name) =>
{
    var item = new Item
    {
        Id = new Random().Next(1000, 9999), // מספר זמני, יש להחליף במספור אוטומטי בהמשך
        Name = name,
        IsComplete = false
    };

    dbContext.Items.Add(item);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/{item.Id}", item);
});

// עדכון משימה (שינוי סטטוס השלמה)
app.MapPut("/{id}", async (ToDoDbContext dbContext, int id, [FromBody] Item updatedItem) =>
{
    var item = await dbContext.Items.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    item.IsComplete = updatedItem.IsComplete; // רק עדכון של שדה IsComplete
    await dbContext.SaveChangesAsync();
    return Results.Ok(item);
});

// מחיקת משימה
app.MapDelete("/{id}", async (ToDoDbContext dbContext, int id) =>
{
    var item = await dbContext.Items.FindAsync(id);
    if (item == null)
    {
        return Results.NotFound();
    }

    dbContext.Items.Remove(item);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
