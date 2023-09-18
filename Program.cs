using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Repositories;
using SaleSavvy_API.Services;
using System.Text;
using SaleSavvy_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAutenticationService, AutenticationService>();
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddSingleton<IAutenticationRepository, AutenticationRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var key = Encoding.ASCII.GetBytes("@ut&tic@do"); 
app.UseAuthorization();

app.Run();