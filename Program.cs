using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using UserApi.Models;
using UserApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserApi.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoDB
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection("UserDatabaseSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>   
{
    var settings = sp.GetRequiredService<IOptions<UserDatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Registrar os serviços necessários

// Cadastro do usuário pelo formulário de cadastro
builder.Services.AddSingleton<ICadastroService, CadastroService>();

// Criar uma tabela única de usuário só com nome, email e senha, sem os demais dados.
builder.Services.AddSingleton<UsuarioService>();

// Personalizar background e fontes, salvar a definição do usuário
builder.Services.AddSingleton<PersonalizacaoUsuarioService>();

// Formulário de feedback
builder.Services.AddSingleton<FeedbackService>();

// Formulário de contato
builder.Services.AddSingleton<ContatoService>();

// Autenticação de Credenciais
builder.Services.AddSingleton<IAutenticacaoLoginService, AutenticacaoLoginService>();

// Armazenar logins realizados pelo usuário
builder.Services.AddSingleton<LogLoginService>();

// Armazenar os dados dos visitantes

builder.Services.AddSingleton<VisitanteAceiteService>();
builder.Services.AddSingleton<VisitanteAnonimoService>();

// Armazena a preferência de idioma do usuário
builder.Services.AddSingleton<PreferenciaIdiomaService>();

// Armazenar as perguntas dos visitantes/usuários
builder.Services.AddSingleton<PesquisaService>();

// Adicionar configuração de autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new ArgumentNullException("JWT Key not found in configuration.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
        };
    });

builder.Services.AddLogging();
builder.Services.AddControllers();

// Adicionar configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Minha API", 
        Version = "v1",
        Description = "API para cadastro de usuário no banco do MongoDB",
        Contact = new OpenApiContact
        {
            Name = "Claudio Silva Bispo",
            Email = "claudio_cssp@hotmail.com",
            Url = new Uri("https://seu-website.com")
        },
        License = new OpenApiLicense
        {
            Name = "Use sob LICX",
            Url = new Uri("https://example.com/license")
        }
    });
});


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(3001); // Escolha uma porta diferente que não esteja em uso
});

var app = builder.Build();

// Testa a conexão ao MongoDB usando UsuarioService
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var usuarioService = services.GetRequiredService<UsuarioService>();
    try
    {
        var users = await usuarioService.GetAll();
        Console.WriteLine($"Conexão ao MongoDB estabelecida com sucesso. {users.Count} usuários encontrados.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao conectar ao MongoDB.");
    }
}

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Certifique-se de adicionar UseAuthentication antes de UseAuthorization
app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();
