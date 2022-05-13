using System.Threading.Tasks;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface IAuthenticateService
    {
        Task<TokenGenerationResponse> GenerateToken(string userId, string jwtToken);
        Task<string> ValidateAuthenticationToken(string userId, string jwtToken);
    }
}
