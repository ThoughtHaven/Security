using System;
using ThoughtHaven.Security.SingleUseTokens.Internal;
using Xunit;

namespace ThoughtHaven.Security.SingleUseTokens.AzureCosmosTable
{
    public class SingleUseTokenRecordTests
    {
        public class Constructor
        {
            public class DataOverload
            {
                [Fact]
                public void NullRecord_Throws()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new SingleUseTokenRecord(data: null!);
                    });
                }

                [Fact]
                public void WhenCalled_SetsValue()
                {
                    var record = new SingleUseTokenData("value",
                        new UtcDateTime(DateTimeOffset.UtcNow));

                    var model = new SingleUseTokenRecord(record);

                    Assert.Equal(record.Value, model.Value);
                }

                [Fact]
                public void WhenCalled_SetsExpiration()
                {
                    var record = new SingleUseTokenData("value",
                        new UtcDateTime(DateTimeOffset.UtcNow));

                    var model = new SingleUseTokenRecord(record);

                    Assert.Equal(record.Expiration.ToOffset(), model.Expiration);
                }
            }
        }

        public class ToDataMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public void WhenCalled_ReturnsRecord()
                {
                    var model = new SingleUseTokenRecord(new SingleUseTokenData(
                        "value", new UtcDateTime(DateTimeOffset.UtcNow)));

                    var record = model.ToData();

                    Assert.Equal(model.Value, record.Value);
                    Assert.Equal(model.Expiration, record.Expiration.ToOffset());
                }
            }
        }
    }
}