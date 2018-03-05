using System;
using System.Threading.Tasks;

namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public abstract class SingleUseTokenServiceBase : ISingleUseTokenService
    {
        private readonly SystemClock _clock;

        protected SingleUseTokenServiceBase(SystemClock clock)
        {
            this._clock = Guard.Null(nameof(clock), clock);
        }

        public virtual Task Create(SingleUseToken token, DateTimeOffset expiration)
        {
            Guard.Null(nameof(token), token);

            var data = new SingleUseTokenData(token, expiration);

            if (IsExpired(data))
            {
                throw new InvalidOperationException("Unable to create this token because it will have already expired.");
            }

            return this.Create(data);
        }

        public virtual async Task<bool> Validate(SingleUseToken token)
        {
            Guard.Null(nameof(token), token);

            var data = await this.Retrieve(token).ConfigureAwait(false);

            if (data != null)
            {
                await this.Delete(data).ConfigureAwait(false);

                if (!IsExpired(data)) { return true; }
            }

            return false;
        }

        protected abstract Task<SingleUseTokenData> Retrieve(SingleUseToken token);
        protected abstract Task Create(SingleUseTokenData data);
        protected abstract Task Delete(SingleUseTokenData data);

        private bool IsExpired(SingleUseTokenData token) =>
            this._clock.UtcNow >= token.Expiration;

        protected class SingleUseTokenData : SingleUseToken
        {
            public DateTimeOffset Expiration { get; }

            public SingleUseTokenData(SingleUseToken token, DateTimeOffset expiration)
                : this(token.Value, expiration)
            { }

            public SingleUseTokenData(string value, DateTimeOffset expiration)
                : base(value)
            {
                this.Expiration = expiration;
            }
        }
    }
}