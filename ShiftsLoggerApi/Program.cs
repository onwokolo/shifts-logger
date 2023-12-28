using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration["ConnectionStrings:DbConnectionString:Development"]
    : builder.Configuration["ConnectionStrings:DbConnectionString:Production"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(dbConnectionString));

// builder.Services.AddSingleton<IShiftRepository, InMemoryShiftRepository>();
builder.Services.AddScoped<IShiftRepository, SqlServerShiftRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
