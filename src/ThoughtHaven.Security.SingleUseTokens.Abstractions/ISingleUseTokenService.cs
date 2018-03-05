using System;
using System.Threading.Tasks;

namespace ThoughtHaven.Security.SingleUseTokens
{
    public interface ISingleUseTokenService
    {
        Task Create(SingleUseToken token, DateTimeOffset expiration);
        Task<bool> Validate(SingleUseToken token);
    }
}