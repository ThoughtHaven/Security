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

        public virtual Task Create(SingleUseToken token, UtcDateTime expiration)
        {
            Guard.Null(nameof(token), token);

            var record = new SingleUseTokenData(token, expiration);

            if (record.IsExpired(this._clock))
            {
                throw new InvalidOperationException("Unable to create this token because it will have already expired.");
            }

            return this.Create(record);
        }

        public virtual async Task<bool> Validate(SingleUseToken token)
        {
            Guard.Null(nameof(token), token);

            var record = await this.Retrieve(token).ConfigureAwait(false);

            if (record != null)
            {
                await this.Delete(record).ConfigureAwait(false);

                if (!record.IsExpired(this._clock)) { return true; }
            }

            return false;
        }

        protected abstract Task<SingleUseTokenData?> Retrieve(SingleUseToken token);
        protected abstract Task Create(SingleUseTokenData record);
        protected abstract Task Delete(SingleUseTokenData record);
    }
}