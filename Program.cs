using Microsoft.OpenApi.Models;
using UserApi.Models;
using UserApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection("UserDatabaseSettings"));
builder.Services.AddSingleton<UserService>();
builder.Services.AddLogging(); // Adiciona suporte para logging
builder.Services.AddControllers(); // Adiciona os controladores ao contêiner de serviços

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

// Testa a conexão ao MongoDB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userService = services.GetRequiredService<UserService>();
    try
    {
        var users = await userService.GetAsync();
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
app.UseAuthorization(); // Adiciona suporte para autorização (se necessário).

// Adicionar o middleware de CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();  // Mapeia as rotas dos controladores

app.Run(); // Inicia o aplicativo
