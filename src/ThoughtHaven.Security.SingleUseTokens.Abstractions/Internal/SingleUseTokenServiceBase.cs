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

            var record = new SingleUseTokenRecord(token, expiration);

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

        protected abstract Task<SingleUseTokenRecord> Retrieve(SingleUseToken token);
        protected abstract Task Create(SingleUseTokenRecord record);
        protected abstract Task Delete(SingleUseTokenRecord record);
    }
}