using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using GifterWebApplication.Interfaces;
using GifterWebApplication.Services;
using GiterWebAPI.Helpers;
using GiterWebAPI.Interfaces;
using GiterWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddTransient<IUserService, UserService>();
    services.AddTransient<IAuthenticationService, AuthenticationService>();
    services.AddTransient<IUserDAL, UserServiceDAL>();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    services.AddDbContext<GifterContextDb>(options => options.UseSqlServer("name=ConnectionStrings:GifterConnectionString"));


}



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.
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
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}
app.MapControllers();

app.Run();
