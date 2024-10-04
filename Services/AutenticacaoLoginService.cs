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

namespace UserApi.Services
{
    public class AutenticacaoLoginService : IAutenticacaoLoginService
    {
        private readonly IMongoCollection<Cliente> _clienteCollection;
        private readonly string _chaveJwt;

        // Registrar logs dos logins
        private readonly LogLoginService _logLoginService;

        public AutenticacaoLoginService(IMongoClient mongoClient, IConfiguration configuracao, LogLoginService logLoginService)
        {
            var database = mongoClient.GetDatabase(configuracao["UserDatabaseSettings:DatabaseName"]);
            _clienteCollection = database.GetCollection<Cliente>(configuracao["UserDatabaseSettings:ClienteCollectionName"]);
            _chaveJwt = configuracao.GetValue<string>("Jwt:Key") ?? throw new ArgumentNullException("Jwt:Key não encontrada na configuração.");
            _logLoginService = logLoginService;
        }

        public async Task<string?> Autenticar(string email, string senha, string tipoLogin)
        {
            var cliente = await _clienteCollection.Find(u => u.email == email && u.senha == senha).FirstOrDefaultAsync();

            if (cliente == null)
                return null;

            // Registrar o log de login
            await _logLoginService.LogAsync(email, tipoLogin);

            var tokenHandler = new JwtSecurityTokenHandler();
            var chave = Encoding.ASCII.GetBytes(_chaveJwt);
            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id_cliente", cliente.id_cliente), new Claim("email", cliente.email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descricaoToken);
            return tokenHandler.WriteToken(token);
        }
    }
}