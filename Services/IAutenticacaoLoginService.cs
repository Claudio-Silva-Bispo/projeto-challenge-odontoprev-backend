using System.Threading.Tasks;

namespace UserApi.Servicos
{
    public interface IAutenticacaoLoginService
    {
        Task<string?> Autenticar(string email, string senha, string tipoLogin);
    }
}
