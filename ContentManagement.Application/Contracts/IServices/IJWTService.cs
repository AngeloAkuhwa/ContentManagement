using ContentManagement.Identity;
using System.Threading.Tasks;

namespace ContentManagement.Application.Contracts
{
    public interface IJWTService
    {
        Task<string> GenerateToken(AppUser user);
    }
}
