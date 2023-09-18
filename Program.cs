using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;
using SaleSavvy_API;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Repositories;
using SaleSavvy_API.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //tipo de autenticação
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        //Tipo de autenticação
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    //requerimento de autenticação
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
            //token que tiver no swagger passa pela api
       new OpenApiSecurityScheme
       {
           Reference = new OpenApiReference
           {
               Type = ReferenceType.SecurityScheme,
               Id = "Bearer" },
           Scheme = "oauth2",
           Name = "Bearer",
           In = ParameterLocation.Header,
       },
       new List<string>()
    }
    });
});

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

var key = Encoding.ASCII.GetBytes(SaleSavvy_API.Key.Secret);

//Porecsso de autenticação na api
builder.Services.AddAuthentication(x =>
{
    //esquema de autenticação
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    //Adiciono como vai ser a autenticação
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

var app = builder.Build();

app.UseAuthentication();

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

app.Run();