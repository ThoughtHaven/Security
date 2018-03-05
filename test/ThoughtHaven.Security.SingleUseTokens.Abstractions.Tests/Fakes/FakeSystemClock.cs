using System;

namespace ThoughtHaven.Security.SingleUseTokens.Abstractions.Tests.Fakes
{
    public class FakeSystemClock : SystemClock
    {
        public override DateTimeOffset UtcNow { get; }

        public FakeSystemClock(DateTimeOffset utcNow) { this.UtcNow = utcNow; }
    }
}