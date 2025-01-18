using Microsoft.AspNetCore.Identity;

namespace NzWalks.API.Services.Interfaces
{
    public interface ITokenRepository
    {
        string CreateToken(IdentityUser user, List<string> roles);
    }
}
