using GI.Core.Entities;

namespace GI.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
