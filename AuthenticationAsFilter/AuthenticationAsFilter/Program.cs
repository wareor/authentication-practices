
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthenticationAsFilter.Configuration;
using AuthenticationAsFilter.Services;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Logging;

var CorsPolicyAuthAsFilter = "_corsPolicyAuthAsFilterAllRequests";

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicyAuthAsFilter,
                      policy =>
                      {
                          policy.AllowAnyOrigin();//JALEF: por ahora se permiten todas las peticiones
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});


builder.Services.Configure<AuthenticationSettings>(
    builder.Configuration.GetSection("AuthenticationAsFilter"));


// Add services to the container.

builder.Services.AddSingleton<UsersService>();


builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
byte[] key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AuthenticationAsFilter").GetValue(typeof(string), "JwtKey").ToString());
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    //x.Audience = "https://localhost:7262"; //JALEF: tal parece que esta configuración aplica solo para audiencias especificas para este servicio, en donde solo una aplicación puede consumir el servicio, es decir, autenticarse y autorizarse su consumo
    //x.Authority = "https://localhost:7262";
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),//JALEF: revisar warning
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


//builder.Service.AddAuthorization()

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication As Filter API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    IdentityModelEventSource.ShowPII=true;
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(CorsPolicyAuthAsFilter);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
