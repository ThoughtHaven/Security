using System;
using Xunit;
using System.Threading.Tasks;
using ThoughtHaven.Security.SingleUseTokens.Fakes;

namespace ThoughtHaven.Security.SingleUseTokens.Internal
{
    public class SingleUseTokenServiceBaseTests
    {
        public class Type
        {
            [Fact]
            public void ImplementsISingleUseTokenService()
            {
                Assert.True(typeof(ISingleUseTokenService).IsAssignableFrom(
                    typeof(SingleUseTokenServiceBase)));
            }
        }

        public class Constructor
        {
            public class ClockOverload
            {
                [Fact]
                public void NullClock_Throws()
                {
                    Assert.Throws<ArgumentNullException>("clock", () =>
                    {
                        new FakeSingleUseTokenServiceBase(clock: null);
                    });
                }
            }
        }

        public class CreateMethod
        {
            public class TokenAndExpirationOverload
            {
                [Fact]
                public async Task NullToken_Throws()
                {
                    var clock = Clock();
                    var service = new FakeSingleUseTokenServiceBase(clock);

                    await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                    {
                        await service.Create(
                            token: null,
                            expiration: clock.UtcNow.AddHours(1));
                    });
                }

                [Fact]
                public async Task ExpirationAlreadyPassed_Throws()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(-1);
                    var service = new FakeSingleUseTokenServiceBase(clock);

                    await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    {
                        await service.Create(token, expiration);
                    });
                }

                [Fact]
                public async Task WhenCalled_CallsCreateOnService()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(1);
                    var service = new FakeSingleUseTokenServiceBase(clock);

                    await service.Create(token, expiration);

                    Assert.Equal(token.Value, service.Create_InputData_Value);
                    Assert.Equal(expiration, service.Create_InputData_Expiration);
                }
            }
        }

        public class ValidateMethod
        {
            public class TokenOverload
            {
                [Fact]
                public async Task NullToken_Throws()
                {
                    var service = new FakeSingleUseTokenServiceBase(Clock());

                    await Assert.ThrowsAsync<ArgumentNullException>("token", async () =>
                    {
                        await service.Validate(token: null);
                    });
                }

                [Fact]
                public async Task ValidToken_ReturnsTrue()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(1);
                    var service = new FakeSingleUseTokenServiceBase(clock);

                    await service.Create(token, expiration);

                    Assert.True(await service.Validate(token));
                }

                [Fact]
                public async Task ValidToken_DeletesToken()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(1);
                    var service = new FakeSingleUseTokenServiceBase(clock);
                    service.Retrieve_Output_Expiration = expiration;

                    await service.Create(token, expiration);

                    await service.Validate(token);

                    Assert.Equal(token.Value, service.Delete_InputData_Value);
                    Assert.Equal(expiration, service.Delete_InputData_Expiration);
                }

                [Fact]
                public async Task TokenNotExists_ReturnsFalse()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var service = new FakeSingleUseTokenServiceBase(clock);
                    service.Retrieve_ShouldReturnData = false;

                    Assert.False(await service.Validate(token));
                }

                [Fact]
                public async Task TokenExpired_ReturnsFalse()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(-1);
                    var service = new FakeSingleUseTokenServiceBase(clock);
                    service.Retrieve_Output_Expiration = expiration;

                    Assert.False(await service.Validate(token));
                }

                [Fact]
                public async Task TokenExpired_DeletesToken()
                {
                    var token = new SingleUseToken("token");
                    var clock = Clock();
                    var expiration = clock.UtcNow.AddHours(-1);
                    var service = new FakeSingleUseTokenServiceBase(clock);
                    service.Retrieve_Output_Expiration = expiration;

                    await service.Validate(token);

                    Assert.Equal(token.Value, service.Delete_InputData_Value);
                    Assert.Equal(expiration, service.Delete_InputData_Expiration);
                }
            }
        }

        private static FakeSystemClock Clock() => new FakeSystemClock(DateTimeOffset.UtcNow);
    }
}