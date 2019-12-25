using System;

namespace ThoughtHaven.Security.SingleUseTokens.Fakes
{
    public class FakeSystemClock : SystemClock
    {
        public override UtcDateTime UtcNow { get; }

        public FakeSystemClock(DateTimeOffset utcNow) { this.UtcNow = utcNow; }
    }
}