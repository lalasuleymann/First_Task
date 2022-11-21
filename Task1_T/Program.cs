using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using Task1_T;
using Task1_T.Data;
using Task1_T.Middlewares;
using Task1_T.Repositories;
using Task1_T.Services.Departments;
using Task1_T.Services.Employees;
using Task1_T.Services.Positions;
using Task1_T.Services.Tokens;
using Task1_T.Services.Users;
using Task1_T.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IDepartmentService, DepartmentManager>();
builder.Services.AddScoped<IPositionService, PositionManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<ITokenService, TokenManager>();
builder.Services.AddSingleton(typeof(JwtSettings), new JwtSettings { Secret = "testSecret", TokenLifeTime = TimeSpan.FromHours(2) });
builder.Services.AddScoped<IEmployeeService, EmployeeManager>();

//validators
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DepartmentValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PermissionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PositionValidator>();


// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(c =>
    c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
});
builder.Services.AddMemoryCache();
// Configure the HTTP request pipeline.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
