using System;
using ThoughtHaven.Security.SingleUseTokens.Fakes;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenRecordTests
    {
        public class Type
        {
            [Fact]
            public void InheritsSingleUseToken()
            {
                Assert.True(typeof(SingleUseToken).IsAssignableFrom(
                    typeof(SingleUseTokenRecord)));
            }
        }

        public class Constructor
        {
            public class TokenAndExpirationOverload
            {
                [Fact]
                public void NullToken_Throws()
                {
                    Assert.Throws<ArgumentNullException>("token", () =>
                    {
                        new SingleUseTokenRecord(
                            token: null!,
                            expiration: new UtcDateTime(DateTimeOffset.UtcNow));
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var record = new SingleUseTokenRecord(new SingleUseToken("value"),
                        expiration: new UtcDateTime(DateTimeOffset.UtcNow));

                    Assert.Equal("value", record.Value);
                }

                [Fact]
                public void WhenCalled_SetsExpiration()
                {
                    var expiration = DateTimeOffset.UtcNow;

                    var record = new SingleUseTokenRecord(new SingleUseToken("value"),
                        new UtcDateTime(expiration));

                    Assert.Equal(expiration.UtcTicks, record.Expiration.Ticks);
                }
            }

            public class ValueAndExpirationOverload
            {
                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var record = new SingleUseTokenRecord("value",
                        expiration: new UtcDateTime(DateTimeOffset.UtcNow));

                    Assert.Equal("value", record.Value);
                }

                [Fact]
                public void WhenCalled_SetsExpiration()
                {
                    var expiration = DateTimeOffset.UtcNow;

                    var record = new SingleUseTokenRecord("value",
                        new UtcDateTime(expiration));

                    Assert.Equal(expiration.UtcTicks, record.Expiration.Ticks);
                }
            }
        }

        public class IsExpiredMethod
        {
            public class ClockOverload
            {
                [Fact]
                public void ExpiredInPast_ReturnsTrue()
                {
                    var clock = Clock();
                    var record = new SingleUseTokenRecord("value",
                        expiration: new UtcDateTime(clock.UtcNow.ToOffset().AddDays(-1)));

                    Assert.True(record.IsExpired(clock));
                }

                [Fact]
                public void ExpiredNow_ReturnsTrue()
                {
                    var clock = Clock();
                    var record = new SingleUseTokenRecord("value", expiration: clock.UtcNow);

                    Assert.True(record.IsExpired(clock));
                }

                [Fact]
                public void ExpiresInFuture_ReturnsFalse()
                {
                    var clock = Clock();
                    var record = new SingleUseTokenRecord("value",
                        expiration: new UtcDateTime(clock.UtcNow.ToOffset().AddDays(1)));

                    Assert.False(record.IsExpired(clock));
                }
            }
        }

        private static FakeSystemClock Clock() => new FakeSystemClock(DateTimeOffset.UtcNow);
    }
}