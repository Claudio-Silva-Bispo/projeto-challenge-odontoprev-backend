using UserApi.Models;
using UserApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.Configure<UserDatabaseSettings>(builder.Configuration.GetSection("UserDatabaseSettings"));
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers();  // Adiciona os controladores ao contêiner de serviços

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

var app = builder.Build();

// Configura o pipeline de requisições HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization(); // Adiciona suporte para autorização (se necessário).

app.MapControllers();  // Mapeia as rotas dos controladores

app.Run(); // Inicia o aplicativo
