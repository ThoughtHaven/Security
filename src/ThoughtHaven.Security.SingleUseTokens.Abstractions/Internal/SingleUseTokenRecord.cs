using System;

namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenRecord : SingleUseToken
    {
        public DateTimeOffset Expiration { get; }

        public SingleUseTokenRecord(SingleUseToken token, DateTimeOffset expiration)
            : this(Guard.Null(nameof(token), token).Value, expiration)
        { }

        public SingleUseTokenRecord(string value, DateTimeOffset expiration) : base(value)
        {
            this.Expiration = expiration;
        }

        public bool IsExpired(SystemClock clock) =>
            Guard.Null(nameof(clock), clock).UtcNow >= this.Expiration;
    }
}