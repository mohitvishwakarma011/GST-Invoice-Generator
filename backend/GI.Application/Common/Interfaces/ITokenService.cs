using GI.Core.Entities;

namespace GI.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(int userId);
    }
}
