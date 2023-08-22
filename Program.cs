using ShoppingListServer.Models;
using ShoppingListServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ShoppingListDatabaseSettings>(
    builder.Configuration.GetSection("ShoppingDatabase"));

// Add services to the container.
builder.Services.AddSingleton<ItemsService>();
builder.Services.AddSingleton<CategoriesService>();
builder.Services.AddSingleton<OrdersService>();

builder.Services.AddControllers();

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5173"));
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

