using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Repositories;
using BolosDoJacquinDb.Services;
using BolosDoJacquinDb.Utils;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do DbContext com SQL Server
builder.Services.AddDbContext<BolosDoJacquinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Mapeamento das configurações do appsettings.json
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// 3. Injeção de Dependência dos Repositórios / Interfaces
builder.Services.AddScoped<ICategorias, CategoriasRepository>();
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<IProdutos, ProdutosRepository>();
builder.Services.AddScoped<IUsuario, UsuariosRepository>();
builder.Services.AddScoped<IAvaliacao, AvaliacaoRepository>();

// 4. Injeção de Dependência dos Serviços
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddHttpClient<SightengineModerationService>();

// 5. Configuração da conta do Cloudinary
var cloudName = builder.Configuration["CloudinarySettings:CloudName"]?.Trim();
var apiKey = builder.Configuration["CloudinarySettings:ApiKey"];
var apiSecret = builder.Configuration["CloudinarySettings:ApiSecret"];

Account account = new Account(cloudName, apiKey, apiSecret);
Cloudinary cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);

// 6. Política de CORS (Apenas um bloco unificado)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 7. Configuração da Autenticação JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "SuaChaveSecretaMuitoLongaESeguraAqui123!";
var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

// 8. Controllers e Swagger com suporte a Bearer Token (Sintaxe para Swashbuckle v10+)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BolosDoJacquinDb API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT neste formato: Bearer {seu_token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer"),
            new List<string>()
        }
    });
});

var app = builder.Build();

// Pipeline de Requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();