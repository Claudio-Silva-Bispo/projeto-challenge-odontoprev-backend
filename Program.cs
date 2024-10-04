using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using UserApi.Models;
using UserApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddSingleton<IClienteService, ClienteService>();

// Formulário de feedback
builder.Services.AddSingleton<IFeedbackService, FeedbackService>();

// Autenticação de Credenciais
builder.Services.AddSingleton<IAutenticacaoLoginService, AutenticacaoLoginService>();

// Armazenar logins realizados pelo usuário
builder.Services.AddSingleton<LogLoginService>();

// Serviços adicionais
builder.Services.AddSingleton<IAgendaService, AgendaService>();
builder.Services.AddSingleton<IClinicaService, ClinicaService>();
builder.Services.AddSingleton<IConsultaService, ConsultaService>();
builder.Services.AddSingleton<IDentistaService, DentistaService>();
builder.Services.AddSingleton<IEstadoCivilService, EstadoCivilService>();
builder.Services.AddSingleton<IFormularioDetalhadoService, FormularioDetalhadoService>();
builder.Services.AddSingleton<INotificacoesService, NotificacoesService>();
builder.Services.AddSingleton<ITipoNotificacaoService, TipoNotificacaoService>();
builder.Services.AddSingleton<ISinistroService, SinistroService>();

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
        Description = "API para projeto do Challenge da OdontoPrev",
        Contact = new OpenApiContact
        {
            Name = "Claudio Silva Bispo e Patricia Naomi",
            Email = "rm553472@fiap.com.br e rm552981@fiap.com.br",
            Url = new Uri("https://github.com/Claudio-Silva-Bispo")
        },
        License = new OpenApiLicense
        {
            Name = "Delfos Machine Group",
            Url = new Uri("https://github.com/Claudio-Silva-Bispo")
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
    var usuarioService = services.GetRequiredService<IClienteService>();
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
