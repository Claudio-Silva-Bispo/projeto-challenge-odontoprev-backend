using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using UserApi.Models;
using System.Threading.Tasks;
using UserApi.Services; // Adicione esta linha para garantir que o namespace correto está sendo usado

namespace UserApi.Servicos
{
    public class AutenticacaoLoginService : IAutenticacaoLoginService
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;
        private readonly string _chaveJwt;

        // Registrar logs dos logins
        private readonly LogLoginService _logLoginService;

        public AutenticacaoLoginService(IMongoClient mongoClient, IConfiguration configuracao, LogLoginService logLoginService)
        {
            var database = mongoClient.GetDatabase(configuracao["UserDatabaseSettings:DatabaseName"]);
            _usuarioCollection = database.GetCollection<Usuario>(configuracao["UserDatabaseSettings:UsersCollectionName"]);
            _chaveJwt = configuracao.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key não encontrada na configuração.");
            _logLoginService = logLoginService;
        }

        public async Task<string?> Autenticar(string email, string senha, string tipoLogin)
        {
            var usuario = await _usuarioCollection.Find(u => u.Email == email && u.Senha == senha).FirstOrDefaultAsync();

            if (usuario == null)
                return null;

            // Registrar o log de login
            await _logLoginService.LogAsync(email, tipoLogin);

            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_chaveJwt);
            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.Id), new Claim("email", usuario.Email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descricaoToken);
            return tokenHandler.WriteToken(token);
        }
    }
}
