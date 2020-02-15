namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenRecord : SingleUseToken
    {
        public UtcDateTime Expiration { get; }

        public SingleUseTokenRecord(SingleUseToken token, UtcDateTime expiration)
            : this(Guard.Null(nameof(token), token).Value, expiration)
        { }

        public SingleUseTokenRecord(string value, UtcDateTime expiration) : base(value)
        {
            this.Expiration = expiration;
        }

        public bool IsExpired(SystemClock clock) =>
            Guard.Null(nameof(clock), clock).UtcNow >= this.Expiration;
    }
}