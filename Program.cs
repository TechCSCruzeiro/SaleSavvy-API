using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;
using SaleSavvy_API.Interface;
using SaleSavvy_API.Models.MovementRecords;
using SaleSavvy_API.Repositories;
using SaleSavvy_API.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //tipo de autenticação

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalleSavy", Version = "v1" });

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
builder.Services.AddSingleton<IProductsService, ProductsService>();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IMovementRecordsService, MovementRecordsService>();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ISalesService, SalesService>();

builder.Services.AddSingleton<IAutenticationRepository, AutenticationRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IMovementRecordsRepository, MovementRecordsRepository>();
builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();
builder.Services.AddSingleton<ISalesRepository, SalesRepository>();

builder.Services.AddSingleton<IRecord, Record>();



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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalleSavy API V1");

        // Configurações adicionais para autenticação com JWT no Swagger
        c.OAuthClientId("swagger-ui");
        c.OAuthClientSecret("swagger-ui-secret");
        c.OAuthRealm("swagger-ui-realm");
        c.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello, world!");
    });
});

app.Run();