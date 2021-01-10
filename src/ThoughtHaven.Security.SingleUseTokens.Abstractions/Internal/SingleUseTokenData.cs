namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenData : SingleUseToken
    {
        public UtcDateTime Expiration { get; }

        public SingleUseTokenData(SingleUseToken token, UtcDateTime expiration)
            : this(Guard.Null(nameof(token), token).Value, expiration)
        { }

        public SingleUseTokenData(string value, UtcDateTime expiration)
            : base(value)
        {
            this.Expiration = expiration;
        }

        public bool IsExpired(SystemClock clock) =>
            Guard.Null(nameof(clock), clock).UtcNow >= this.Expiration;
    }
}